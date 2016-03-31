using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;
using Gear.Infrastructure.MultiTenancy;
using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.TenantModule;

namespace ReportMS.Domain.Models.AccountModule
{
    /// <summary>
    /// 角色（聚合根）
    /// </summary>
    public class Role : AggregateRoot, IMayHaveTenant, ISoftDelete, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取角色名
        /// </summary>
        public string RoleName { get; private set; }

        /// <summary>
        /// 获取角色显示名称
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 获取角色描述
        /// </summary>
        public string Description { get; private set; }

        #region IMayHaveTenant<Guid> Members

        /// <summary>
        /// 获取租户 Id.
        /// 若租户为 Null，表示为管理员（不分租户）
        /// </summary>
        public Guid? TenantId { get; private set; }

        /// <summary>
        /// 获取租户信息
        /// </summary>
        public virtual Tenant Tenant { get; private set; }

        #endregion

        #region ISoftDelete Members

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示此角色是否可用
        /// </summary>
        public bool Enabled { get; private set; }

        #endregion

        /// <summary>
        /// 获取创建人
        /// </summary>
        public string CreatedBy { get; private set; }

        /// <summary>
        /// 获取创建时间
        /// </summary>
        public DateTime? CreatedDate { get; private set; }

        /// <summary>
        /// 获取更新人
        /// </summary>
        public string UpdatedBy { get; private set; }

        /// <summary>
        /// 获取更新时间
        /// </summary>
        public DateTime? UpdatedDate { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>Role</c>实例。仅供 Lazy 使用
        /// </summary>
        public Role()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>Role</c>实例
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="displayName">角色显示名</param>
        /// <param name="description">角色描述</param>
        /// <param name="tenantId">租户id</param>
        /// <param name="createdBy">创建人</param>
        public Role(string roleName, string displayName, string description, Guid? tenantId, string createdBy)
        {
            this.RoleName = roleName;
            this.DisplayName = displayName;
            this.Description = description;
            this.TenantId = tenantId;
            this.CreatedBy = createdBy;
            this.CreatedDate = DateTime.Now;

            this.GenerateNewIdentity();
            this.Enable();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="displayName">角色显示名</param>
        /// <param name="description">角色描述</param>
        /// <param name="updatedBy">更新人</param>
        public void UpdateRole(string displayName, string description, string updatedBy)
        {
            this.DisplayName = displayName;
            this.Description = description;
            this.UpdatedBy = updatedBy;
            this.UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// 是否是管理员（不区分租户）
        /// </summary>
        /// <returns>True 表示是管理员；否则为 false</returns>
        public bool IsAdmin()
        {
            // Todo: use role level to present the role level, as administator, manager, register etc.
            return !this.TenantId.HasValue;
        }

        /// <summary>
        /// 启用此角色
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
                this.Enabled = true;
        }

        /// <summary>
        /// 禁用此角色
        /// </summary>
        public void Disable()
        {
            if (this.Enabled)
                this.Enabled = false;
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(this.RoleName))
                yield return new ValidationResult("The role name is null or empty.");

            if (this.TenantId == Guid.Empty)
                yield return new ValidationResult("The id of tenant is guid empty.");
        }

        #endregion
    }
}
