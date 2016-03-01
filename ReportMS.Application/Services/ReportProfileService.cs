using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool ExistReportProfile(string reportProfileName)
        {
            var spec = Specification<ReportProfile>.Eval(p => p.Name.Equals(reportProfileName, StringComparison.OrdinalIgnoreCase));
            return this._reportProfileRepository.Exist(spec);
        }

        public void AddReportProfile(ReportProfileDto profileDto)
        {
            var reportProfile = new ReportProfile(profileDto.Name, profileDto.Description, profileDto.ReportId, profileDto.CreatedBy);
            this._reportProfileRepository.Add(reportProfile);
        }

        public void UpdateReportProfile(Guid profileId, string name, string description, string updatedBy)
        {
            var reportProfile = this._reportProfileRepository.GetByKey(profileId);
            reportProfile.UpdateProfileHeader(name, description, updatedBy);
            this._reportProfileRepository.Update(reportProfile);
        }

        public void SetProfileFields(Guid profileId, IEnumerable<string> fields)
        {
            var reportProfile = this._reportProfileRepository.GetByKey(profileId);
            if (reportProfile == null)
                return;

            var existFields = reportProfile.ReportProfileFields;
            if (existFields != null)
                this._reportProfileRepository.RemoveProfileFields(existFields); ;

            if (fields != null)
            {
                var profileFields = (from field in fields
                    select new ReportProfileField(profileId, field));
                reportProfile.AddProfileFields(profileFields);
            }

            this._reportProfileRepository.Update(reportProfile);
        }

        public void RemoveReportProfile(Guid reportProfileId)
        {
            var profile = this._reportProfileRepository.GetByKey(reportProfileId);
            if (profile == null)
                return;
            profile.Disable();
            this._reportProfileRepository.Update(profile);
        }

        public void RemoveReportProfileField(Guid profileId, Guid profileFieldId)
        {
            var profile = this._reportProfileRepository.GetByKey(profileId);
            if (profile == null)
                return;

            var profileField = profile.ReportProfileFields.SingleOrDefault(f => f.ID == profileFieldId);
            if (profileField != null)
                this._reportProfileRepository.RemoveProfileField(profileField);
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
