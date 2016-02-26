using System;
using System.Collections.Generic;
using System.Text;
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

        /// <summary>
        /// 获取所有的报表数据
        /// </summary>
        /// <returns>报表数据集合</returns>
        public IEnumerable<ReportDto> GetReports()
        {
            var sqlQuery = "SELECT ReportId AS ID, ReportName, DisplayName, Description, Database_Name AS [Database], Database_Schema AS [Schema] ";
            sqlQuery += " FROM  RMS_Report ";
            sqlQuery += " WHERE Enabled = 1 ";
            return DatabaseReader.Default.Reader.Select<ReportDto>(sqlQuery);
        }

        /// <summary>
        /// 获取指定的角色所拥有的所有报表，不包含字段
        /// </summary>
        /// <param name="roleId">指定的角色 Id</param>
        /// <returns>报表数据集合</returns>
        public IEnumerable<ReportDto> GetReports(Guid roleId)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine("SELECT rep.ReportId AS ID, rep.ReportName, rep.DisplayName, rep.Description, rep.Database_Name AS [Database], rep.Database_Schema AS [Schema] ");
            sqlQuery.AppendLine(" FROM  RMS_Role ro ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroupRole rgr ON rgr.RoleId = ro.RoleId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroup rg ON rg.ReportGroupId = rgr.ReportGroupId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroupItem rgi ON rgi.ReportGroupId = rg.ReportGroupId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportProfile rp ON rp.ReportProfileId = rgi.ReportProfileId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_Report rep ON rep.ReportId = rp.ReportId ");
            sqlQuery.AppendLine(" WHERE ro.Enabled = 1 AND rep.Enabled = 1 ");
            sqlQuery.AppendLine("   AND ro.RoleId = @RoleId ");

            return DatabaseReader.Default.Reader.Select<ReportDto>(sqlQuery.ToString(), new { RoleId = roleId });
        }

        /// <summary>
        /// 获取报表数据
        /// </summary>
        /// <param name="reportId">报表 Id</param>
        /// <param name="includeFields">是否包含关联项（Field），默认包含</param>
        /// <returns>报表</returns>
        public ReportDto GetReport(Guid reportId, bool includeFields = true)
        {
            var sqlQuery = "SELECT ReportId AS ID, ReportName, DisplayName, Description, Database_Name AS [Database], Database_Schema AS [Schema] ";
            sqlQuery += " FROM  RMS_Report ";
            sqlQuery += " WHERE Enabled = 1 AND ReportId = @ReportId";

            var report = DatabaseReader.Default.Reader.SelectFirstOrDefault<ReportDto>(sqlQuery, new { ReportId = reportId });

            if (includeFields)
            {
                var fields = this.GetReportFields(reportId);
                report.Fields = fields;
            }

            return report;
        }

        /// <summary>
        /// 获取在指定角色中的指定报表, 包含字段
        /// </summary>
        /// <param name="roleId">指定的角色</param>
        /// <param name="reportId">指定的报表</param>
        /// <returns>报表</returns>
        public ReportDto GetReport(Guid roleId, Guid reportId)
        {
            var report = this.GetReport(reportId, false);  // 获取报表头
            if (report == null)
                return null;

            // 获取报表字段，并进行筛选
            var fields = this.GetReportFields(roleId, reportId);  
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

        private IEnumerable<ReportFieldDto> GetReportFields(Guid roleId, Guid reportId)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine("SELECT rf.ReportFieldId AS ID, rf.ReportId, rf.FieldName, rf.DisplayName, rf.DataType, rf.Sort ");
            sqlQuery.AppendLine(" FROM  RMS_Role ro ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroupRole rgr ON rgr.RoleId = ro.RoleId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroup rg ON rg.ReportGroupId = rgr.ReportGroupId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportGroupItem rgi ON rgi.ReportGroupId = rg.ReportGroupId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportProfile rp ON rp.ReportProfileId = rgi.ReportProfileId");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportProfileField rpf ON rpf.ReportProfileId = rp.ReportProfileId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_Report rep ON rep.ReportId = rp.ReportId ");
            sqlQuery.AppendLine("       INNER JOIN RMS_ReportField rf ON rf.ReportId = rep.ReportId AND rf.FieldName = rpf.FieldName ");
            sqlQuery.AppendLine(" WHERE ro.Enabled = 1 AND rg.Enabled = 1 AND rp.Enabled = 1 AND rep.Enabled = 1 ");
            sqlQuery.AppendLine("   AND ro.RoleId = @RoleId AND rep.ReportId = @ReportId");

            return DatabaseReader.Default.Reader.Select<ReportFieldDto>(sqlQuery.ToString(), new { RoleId = roleId, ReportId = reportId });
        }

        #endregion
    }
}
