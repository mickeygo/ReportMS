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
        private readonly IReportGroupItemRepository _reportGroupItemRepository;

        #endregion

        #region Ctor

        public ReportService(IReportRepository reportRepository, IReportGroupRoleRepository reportGroupRoleRepository,
            IReportGroupRepository reportGroupRepository, IReportGroupItemRepository reportGroupItemRepository)
        {
            this._reportRepository = reportRepository;
            this._reportGroupRoleRepository = reportGroupRoleRepository;
            this._reportGroupRepository = reportGroupRepository;
            this._reportGroupItemRepository = reportGroupItemRepository;
        }

        #endregion

        #region IReportService Members

        public IEnumerable<ReportDto> FindAllReport()
        {
            return this._reportRepository.FindAll().MapAs<ReportDto>();
        }

        public ReportDto FindReport(Guid reportId)
        {
            return this._reportRepository.GetByKey(reportId).MapAs<ReportDto>();
        }

        public ReportDto FindReport(string reportName)
        {
            var spec = Specification<Report>.Eval(r => r.ReportName.Equals(reportName, StringComparison.OrdinalIgnoreCase));
            return this._reportRepository.Find(spec).MapAs<ReportDto>();
        }

        public ReportDto CreateReport(ReportDto reportDto)
        {
            var report = new Report(reportDto.ReportName, reportDto.DisplayName, reportDto.Description,
                reportDto.Database, reportDto.Schema, reportDto.CreatedBy);
            this._reportRepository.Add(report);

            return report.MapAs<ReportDto>();
        }

        public ReportDto UpdateReport(ReportDto reportDto)
        {
            var report = this._reportRepository.GetByKey(reportDto.ID);
            if (report == null) 
                return null;

            report.UpdateReport(reportDto.DisplayName, reportDto.Description, reportDto.Database, reportDto.Schema,
                reportDto.UpdatedBy);

            return report.MapAs<ReportDto>();
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

        public void RemoveAllThenAddFields(Guid reportId, IEnumerable<ReportFieldDto> fieldDtos)
        {
            var report = this._reportRepository.GetByKey(reportId);
            if (report == null)
                return;

            var fields = report.Fields;
            if (fields == null || !fields.Any())
                return;

            this._reportRepository.RemoveFileds(fields);

            var index = 1;
            var addingFields = (from f in fieldDtos
                select new ReportField(report.ID, f.FieldName, f.DisplayName, f.DataType, index++, f.CreatedBy));
            report.AddFields(addingFields);

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
            if (this._reportGroupItemRepository != null)
                this._reportGroupItemRepository.Context.Dispose();
        }

        #endregion
    }
}
