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


        #endregion

        #region Private Methods



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
