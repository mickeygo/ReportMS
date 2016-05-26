using System;
using System.Collections.Generic;
using System.Text;
using Gear.Infrastructure.Algorithms.Cryptography;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Reports.Dao;
using ReportMS.ServiceContracts;

namespace ReportMS.Reports.Services
{
    /// <summary>
    /// 报表查询服务
    /// </summary>
    public class ReportQueryService : IReportQueryService
    {
        #region IReportQueryServiceContract Members

        public IEnumerable<ReportDto> GetReports()
        {
            var sqlQuery = "SELECT ReportId AS ID, ReportName, DisplayName, Description, [Schema] ";
            sqlQuery += " FROM  RMS_Report ";
            sqlQuery += " WHERE Enabled = 1 ";

            return DatabaseReader.Default.Reader.Select<ReportDto>(sqlQuery);
        }


        public IEnumerable<ReportDto> GetReports(Guid roleId)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine("SELECT rep.ReportId AS ID, rep.ReportName, rep.DisplayName, rep.Description, [Schema] ");
            sqlQuery.AppendLine(" FROM  RMS_Role ro ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroupRole rgr ON rgr.RoleId = ro.RoleId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroup rg ON rg.ReportGroupId = rgr.ReportGroupId AND rg.Enabled = 1 ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroupItem rgi ON rgi.ReportGroupId = rg.ReportGroupId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportProfile rp ON rp.ReportProfileId = rgi.ReportProfileId AND rp.Enabled = 1 ");
            sqlQuery.AppendLine("       INNER JOIN RMS_Report rep ON rep.ReportId = rp.ReportId AND rep.Enabled = 1 ");
            sqlQuery.AppendLine(" WHERE ro.Enabled = 1 ");
            sqlQuery.AppendLine("   AND ro.RoleId = @RoleId ");

            return DatabaseReader.Default.Reader.Select<ReportDto>(sqlQuery.ToString(), new { RoleId = roleId });
        }

        public IEnumerable<ReportProfileDto> GetReportProfiles(Guid roleId)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine("SELECT rp.ReportProfileId AS ID, rp.Name, rp.Description, rp.ReportId ");
            sqlQuery.AppendLine(" FROM  RMS_Role ro ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroupRole rgr ON rgr.RoleId = ro.RoleId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroup rg ON rg.ReportGroupId = rgr.ReportGroupId AND rg.Enabled = 1 ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroupItem rgi ON rgi.ReportGroupId = rg.ReportGroupId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportProfile rp ON rp.ReportProfileId = rgi.ReportProfileId AND rp.Enabled = 1 ");
            sqlQuery.AppendLine("       INNER JOIN RMS_Report rep ON rep.ReportId = rp.ReportId AND rep.Enabled = 1 ");
            sqlQuery.AppendLine(" WHERE ro.Enabled = 1 ");
            sqlQuery.AppendLine("   AND ro.RoleId = @RoleId ");

            return DatabaseReader.Default.Reader.Select<ReportProfileDto>(sqlQuery.ToString(), new { RoleId = roleId });
        }

        public ReportDto GetReport(Guid reportId, bool includeFields = true)
        {
            var sqlQuery = "SELECT ReportId AS ID, ReportName, DisplayName, Description ";
            sqlQuery += " FROM  RMS_Report ";
            sqlQuery += " WHERE Enabled = 1 AND ReportId = @ReportId";

            var report = DatabaseReader.Default.Reader.SelectFirstOrDefault<ReportDto>(sqlQuery, new { ReportId = reportId });
            report.Rdbms = this.GetRdbms(reportId);

            if (includeFields)
            {
                var fields = this.GetReportFields(reportId);
                report.Fields = fields;
            }

            return report;
        }

        public ReportDto GetReportWithProfile(Guid reportProfileId)
        {
            var report = this.GetReportViaProfile(reportProfileId);
            if (report == null)
                return null;

            var fields = this.GetFieldsViaProfile(reportProfileId);
            if (fields != null)
                report.Fields = fields;

            return report;
        }

        #endregion

        #region Private Methods

        private IEnumerable<ReportFieldDto> GetReportFields(Guid reportId)
        {
            var sqlQuery = "SELECT ReportFieldId AS ID, ReportId, FieldName, DisplayName, DataType, Sort ";
            sqlQuery += " FROM  RMS_ReportField ";
            sqlQuery += " WHERE ReportId = @ReportId ";
            sqlQuery += " ORDER BY Sort";
            return DatabaseReader.Default.Reader.Select<ReportFieldDto>(sqlQuery, new { ReportId = reportId });
        }

        private ReportDto GetReportViaProfile(Guid reportProfileId)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine("SELECT rep.ReportId AS ID, rep.ReportName, rep.DisplayName, rep.Description ");
            sqlQuery.AppendLine(" FROM  RMS_Report rep ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportProfile rp ON rp.ReportId = rep.ReportId AND rp.Enabled = 1 ");
            sqlQuery.AppendLine(" WHERE rep.Enabled = 1 AND rp.ReportProfileId = @ReportProfileId ");

            var report = DatabaseReader.Default.Reader.SelectFirstOrDefault<ReportDto>(sqlQuery.ToString(), new { ReportProfileId = reportProfileId });
            report.Rdbms = this.GetRdbms(report.ID);

            return report;
        }

        private IEnumerable<ReportFieldDto> GetFieldsViaProfile(Guid reportProfileId)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine("SELECT rf.ReportFieldId AS ID, rf.ReportId, rf.FieldName, rf.DisplayName, rf.DataType, rf.Sort ");
            sqlQuery.AppendLine(" FROM  RMS_ReportProfile rp ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportProfileField rpf ON rpf.ReportProfileId = rp.ReportProfileId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_Report rep ON rep.ReportId = rp.ReportId AND rep.Enabled = 1 ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportField rf ON rf.ReportId = rep.ReportId AND rf.FieldName = rpf.FieldName ");
            sqlQuery.AppendLine(" WHERE rp.Enabled = 1 AND rp.ReportProfileId = @ReportProfileId ");

            return DatabaseReader.Default.Reader.Select<ReportFieldDto>(sqlQuery.ToString(), new { ReportProfileId = reportProfileId });
        }

        private RdbmsDto GetRdbms(Guid reportId)
        {
            var sqlQuery = "SELECT rdbms.RdbmsId AS ID, rdbms.Name, rdbms.Description, rdbms.Server, rdbms.Catalog, rdbms.UserId, rdbms.Password, rdbms.Provider, rdbms.ReadOnly ";
            sqlQuery += " FROM  RMS_Rdbms rdbms ";
            sqlQuery += " INNER JOIN RMS_Report report ON report.RdbmsId = rdbms.RdbmsId ";
            sqlQuery += " WHERE report.Enabled = 1 AND rdbms.Enabled = 1 AND report.ReportId = @ReportId";

            var rdbms = DatabaseReader.Default.Reader.SelectFirstOrDefault<RdbmsDto>(sqlQuery, new { ReportId = reportId });
            if (rdbms == null)
                return null;

            rdbms.UserId = CryptoFactory.AES.Decrypt(rdbms.UserId);
            rdbms.Password = CryptoFactory.AES.Decrypt(rdbms.Password);

            return rdbms;
        }

        #endregion
    }
}
