using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure.Specifications;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories;
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

        #endregion

        #region Ctor

        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            this._userRepository = userRepository;
            this._userRoleRepository = userRoleRepository;
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

        public void SetRoles(Guid userId, Guid? roleId, string creator)
        {
            var spec = Specification<UserRole>.Eval(u => u.UserId == userId);

            var userRoles = this._userRoleRepository.FindAll(spec);
            if (userRoles != null)
            {
                foreach (var userRole in userRoles)
                    this._userRoleRepository.Remove(userRole);
            }

            if (!roleId.HasValue)
                return;

            // Todo: check the user whether has more than one role in the tennant that the role own to, in a role domain service.
            var addUserRole = new UserRole(userId, roleId.Value, creator);
            this._userRoleRepository.Add(addUserRole);
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
