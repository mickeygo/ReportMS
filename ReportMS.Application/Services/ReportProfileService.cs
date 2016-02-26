using System;
using System.Collections.Generic;
using Gear.Infrastructure.Specifications;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.ReportModule.ReportProfileAggregate;
using ReportMS.Domain.Repositories;
using ReportMS.ServiceContracts;

namespace ReportMS.Application.Services
{
    public class ReportProfileService : IReportProfileService
    {
        #region Private Fields

        private readonly IReportProfileRepository _reportProfileRepository;

        #endregion

        #region Ctor

        public ReportProfileService(IReportProfileRepository reportProfileRepository)
        {
            _reportProfileRepository = reportProfileRepository;
        }

        #endregion

        #region IReportProfileService Members

        public IEnumerable<ReportProfileDto> FindAllReportProfile()
        {
            return this._reportProfileRepository.FindAll().MapAs<ReportProfileDto>();
        }

        public ReportProfileDto FindReportProfile(Guid reportProfileId)
        {
            return this._reportProfileRepository.GetByKey(reportProfileId).MapAs<ReportProfileDto>();
        }

        public ReportProfileDto FindReportProfile(string reportProfileName)
        {
            var spec = Specification<ReportProfile>.Eval(p => p.Name.Equals(reportProfileName, StringComparison.OrdinalIgnoreCase));
            return this._reportProfileRepository.Find(spec).MapAs<ReportProfileDto>();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._reportProfileRepository != null)
                this._reportProfileRepository.Context.Dispose();
        }

        #endregion
    }
}
