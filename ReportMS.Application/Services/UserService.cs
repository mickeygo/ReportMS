using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure.Specifications;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories;
using ReportMS.Domain.Services;
using ReportMS.ServiceContracts;

namespace ReportMS.Application.Services
{
    /// <summary>
    /// 表示用户应用服务。
    /// 包含用户及角色查找
    /// </summary>
    public class UserService : IUserService
    {
        #region Private Fields

        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserDomainService _userDomainService;

        #endregion

        #region Ctor

        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository,
            IUserDomainService userDomainService)
        {
            this._userRepository = userRepository;
            this._userRoleRepository = userRoleRepository;
            this._userDomainService = userDomainService;
        }

        #endregion

        #region IUserService Members

        public UserDto FindUser(Guid userId)
        {
            return this._userRepository.GetByKey(userId).MapAs<UserDto>();
        }

        public UserDto FindUser(string userName)
        {
            var spec = Specification<User>.Eval(u => u.UserName == userName);
            return this._userRepository.Find(spec).MapAs<UserDto>();
        }

        public IEnumerable<RoleDto> FindRoles(Guid userId)
        {
            var spec = Specification<UserRole>.Eval(u => u.UserId == userId);
            var userRoles = this._userRoleRepository.FindAll(spec);
            return userRoles.Select(u => u.Role).ToList().MapAs<RoleDto>();
        }

        public RoleDto FindRole(Guid userId, Guid tenantId)
        {
            var spec = Specification<UserRole>.Eval(u => u.UserId == userId);
            var userRoles = this._userRoleRepository.FindAll(spec);
            return userRoles.Select(u => u.Role).SingleOrDefault(r => r.TenantId == tenantId).MapAs<RoleDto>();
        }

        public void SetRoles(Guid userId, Guid tenantId, Guid? roleId, string creator)
        {
            this._userDomainService.SetRoles(this._userRoleRepository, userId, tenantId, roleId, creator);
        }

        public bool IsAdmin(Guid userId)
        {
            var spec = Specification<UserRole>.Eval(u => u.UserId == userId && u.Role.IsAdmin());
            return this._userRoleRepository.Exist(spec);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._userRepository != null)
                this._userRepository.Context.Dispose();
            if (this._userRoleRepository != null)
                this._userRoleRepository.Context.Dispose();
        }

        #endregion
    }
}
