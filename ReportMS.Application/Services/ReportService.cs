using System;
using System.Collections.Generic;
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

        public void CreateReport(ReportDto reportDto)
        {
            var report = new Report(reportDto.ReportName, reportDto.DisplayName, reportDto.Description,
                reportDto.Database, reportDto.Schema, reportDto.CreatedBy);

            this._reportRepository.Add(report);
        }

        public void UpdateReport(ReportDto reportDto)
        {
            var report = this._reportRepository.GetByKey(reportDto.ID);
        }

        public void DeleteReport(Guid reportId)
        {
            var report = this._reportRepository.GetByKey(reportId);
            report.Disable();
        }

        public void RemoveField(Guid fieldId)
        {
            this._reportRepository.RemoveFiled(fieldId);
        }

        #endregion

        #region Private Methods

        private void TransferReport(Report report, ReportDto reportDto)
        {
            
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
