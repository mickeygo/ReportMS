using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure.Specifications;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories;
using ReportMS.Domain.Repositories.Specifications;
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
            var spec = UserSpecification.FindUser(userName);
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

        public bool IsExistUser(string userName)
        {
            var spec = UserSpecification.FindUser(userName);
            return this._userRepository.Exist(spec);
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

        public void CreateUser(UserDto userDto)
        {
            if (this.IsExistUser(userDto.UserName))
                return;

            var user = new User(userDto.UserName, userDto.Password, userDto.EmployeeNo, userDto.Email,
                userDto.EnglishName, userDto.LocalName,
                userDto.Company, userDto.Organization, userDto.OrganizationDescription, userDto.Department, userDto.Job,
                userDto.Tel, userDto.Extension, userDto.VOIP, userDto.OnBoardDate, userDto.Manager, userDto.Agent,
                userDto.Grade, userDto.Shift, userDto.CreatedBy);

            this._userRepository.Add(user);
        }

        public void UpdateUser(UserDto userDto)
        {
            var spec = UserSpecification.FindUser(userDto.UserName);
            var user = this._userRepository.Find(spec);
            if (user != null)
            {
                user.Update(userDto.Password, userDto.EmployeeNo, userDto.Email,
                    userDto.EnglishName, userDto.LocalName,
                    userDto.Company, userDto.Organization, userDto.OrganizationDescription, userDto.Department,
                    userDto.Job,
                    userDto.Tel, userDto.Extension, userDto.VOIP, userDto.OnBoardDate, userDto.Manager, userDto.Agent,
                    userDto.Grade, userDto.Shift, userDto.UpdatedBy);

                this._userRepository.Update(user);
            }
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
