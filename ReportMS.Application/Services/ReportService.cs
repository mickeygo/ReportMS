using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure.Specifications;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;
using ReportMS.Domain.Repositories;
using ReportMS.ServiceContracts;

namespace ReportMS.Application.Services
{
    /// <summary>
    /// 报表应用服务
    /// </summary>
    public class ReportService : IReportService
    {
        #region Private Fields

        private readonly IReportRepository _reportRepository;
        private readonly IReportGroupRoleRepository _reportGroupRoleRepository;
        private readonly IReportGroupRepository _reportGroupRepository;

        #endregion

        #region Ctor

        public ReportService(IReportRepository reportRepository, IReportGroupRoleRepository reportGroupRoleRepository,
            IReportGroupRepository reportGroupRepository)
        {
            this._reportRepository = reportRepository;
            this._reportGroupRoleRepository = reportGroupRoleRepository;
            this._reportGroupRepository = reportGroupRepository;
        }

        #endregion

        #region IReportService Members

        public IEnumerable<ReportDto> FindAllReport()
        {
            return this._reportRepository.FindAll().ToList().MapAs<ReportDto>();
        }

        public ReportDto FindReport(Guid reportId)
        {
            return this._reportRepository.GetByKey(reportId).MapAs<ReportDto>();
        }

        public bool ExistReport(string reportName)
        {
            var spec = Specification<Report>.Eval(r => r.ReportName.Equals(reportName, StringComparison.OrdinalIgnoreCase));
            return this._reportRepository.Exist(spec);
        }

        public ReportDto CreateReport(ReportDto reportDto)
        {
            var report = new Report(reportDto.ReportName, reportDto.DisplayName, reportDto.Description,
                reportDto.Database, reportDto.Schema, reportDto.CreatedBy);
            this._reportRepository.Add(report);

            return report.MapAs<ReportDto>();
        }

        public void UpdateReportHeader(Guid reportId, string displayName, string description, string updatedBy)
        {
            var report = this._reportRepository.GetByKey(reportId);
            if (report == null)
                return;

            report.UpdateReport(displayName, description, updatedBy);
            this._reportRepository.Update(report);
        }

        public void DeleteReport(Guid reportId)
        {
            var report = this._reportRepository.GetByKey(reportId);
            if (report != null)
            {
                report.Disable();
                this._reportRepository.Update(report);
            }
        }

        public void RemoveField(Guid fieldId)
        {
            this._reportRepository.RemoveFiled(fieldId);
        }

        public void SetReportFields(Guid reportId, IEnumerable<ReportFieldDto> fieldDtos)
        {
            var report = this._reportRepository.GetByKey(reportId);
            if (report == null)
                return;

            var fields = report.Fields;
            if (fields != null && fields.Any())
                this._reportRepository.RemoveFileds(fields);

            if (fieldDtos != null)
            {
                var index = 1;
                var addingFields = (from f in fieldDtos
                    select new ReportField(report.ID, f.FieldName, f.DisplayName, f.DataType, index++, f.CreatedBy));
                report.AddFields(addingFields);
            }

            this._reportRepository.Update(report);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._reportRepository != null)
                this._reportRepository.Context.Dispose();
            if (this._reportGroupRoleRepository != null)
                this._reportGroupRoleRepository.Context.Dispose();
            if (this._reportGroupRepository != null)
                this._reportGroupRepository.Context.Dispose();
        }

        #endregion
    }
}
