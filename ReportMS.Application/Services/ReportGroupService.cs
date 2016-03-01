﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure.Specifications;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;
using ReportMS.Domain.Repositories;
using ReportMS.ServiceContracts;

namespace ReportMS.Application.Services
{
    /// <summary>
    /// 报表分组管理服务类。
    /// 该服务用于对报表分组的管理
    /// </summary>
    public class ReportGroupService : IReportGroupService
    {
        #region Private Fields

        private readonly IReportGroupRepository _reportGroupRepository;
        private readonly IReportGroupRoleRepository _reportGroupRoleRepository;

        #endregion

        #region Ctor

        public ReportGroupService(IReportGroupRepository reportGroupRepository, 
            IReportGroupRoleRepository reportGroupRoleRepository)
        {
            _reportGroupRepository = reportGroupRepository;
            _reportGroupRoleRepository = reportGroupRoleRepository;
        }

        #endregion

        #region IReportGroupService Members

        public IEnumerable<ReportGroupDto> FindReportGroups()
        {
            return this._reportGroupRepository.FindAll().MapAs<ReportGroupDto>();
        }

        public IEnumerable<ReportGroupDto> FindReportGroupInRole(Guid roleId)
        {
            var reportGroupRoles = this._reportGroupRoleRepository.FindAll(Specification<ReportGroupRole>.Eval(r => r.RoleId == roleId));
            return (from reportGroupRole in reportGroupRoles
                select this._reportGroupRepository.GetByKey(reportGroupRole.ReportGroupId)).MapAs<ReportGroupDto>();
        }

        public ReportGroupDto FindReportGroup(Guid reportGroupId)
        {
            return this._reportGroupRepository.GetByKey(reportGroupId).MapAs<ReportGroupDto>();
        }

        public bool ExistReportGroup(string reportGroupName)
        {
            var spec =
                Specification<ReportGroup>.Eval(
                    r => r.GroupName.Equals(reportGroupName, StringComparison.OrdinalIgnoreCase));
            return this._reportGroupRepository.Exist(spec);
        }

        public void CreateReportGroup(ReportGroupDto reportGroupDto)
        {
            var reportGroup = new ReportGroup(reportGroupDto.GroupName, reportGroupDto.DisplayName,
                reportGroupDto.Description, reportGroupDto.CreatedBy);
            this._reportGroupRepository.Add(reportGroup);
        }

        public void UpdateReportGroup(ReportGroupDto reportGroupDto)
        {
            var reportGroup = this._reportGroupRepository.GetByKey(reportGroupDto.ID);
            if (reportGroup == null)
                return;

            reportGroup.UpdateGroupHeader(reportGroupDto.DisplayName, reportGroupDto.Description,
                reportGroupDto.UpdatedBy);
            this._reportGroupRepository.Update(reportGroup);
        }

        public void RemoveReportGroup(Guid reportGroupId)
        {
            var reportGroup = this._reportGroupRepository.GetByKey(reportGroupId);
            if (reportGroup == null)
                return;

            reportGroup.Disable();
            this._reportGroupRepository.Update(reportGroup);
        }

        public void AddReportGroupItems(Guid reportGroupId, IEnumerable<Guid> reportProfileIds)
        {
            var reportGroup = this._reportGroupRepository.GetByKey(reportGroupId);
            if (reportGroup == null)
                return;

            var items = (from reportProfileId in reportProfileIds
                where reportGroup.ReportGroupItems.All(i => i.ReportProfileId != reportProfileId)
                select new ReportGroupItem(reportGroupId, reportProfileId));

            reportGroup.AddGroupItems(items);
            this._reportGroupRepository.Update(reportGroup);
        }

        public void RemoveReportGroupItem(Guid reportGroupId, Guid reportGroupItemId)
        {
            var reportGroup = this._reportGroupRepository.GetByKey(reportGroupId);
            var item = reportGroup.ReportGroupItems.SingleOrDefault(r => r.ID == reportGroupItemId);
            if (item == null)
                return;
            
            this._reportGroupRepository.RemoveReportGroupItem(item);
            this._reportGroupRepository.Update(reportGroup);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._reportGroupRepository != null)
                this._reportGroupRepository.Context.Dispose();
            if (this._reportGroupRoleRepository != null)
                this._reportGroupRoleRepository.Context.Dispose();
        }

        #endregion
    }
}
