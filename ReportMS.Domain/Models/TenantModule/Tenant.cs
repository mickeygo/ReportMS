using System;
using Gear.Infrastructure;
using Gear.Infrastructure.Repositories;

namespace ReportMS.Domain.Models.TenantModule
{
    /// <summary>
    /// 系统租户（聚合根）
    /// </summary>
    public class Tenant : AggregateRoot, ISoftDelete
    {
        #region Properties

        /// <summary>
        /// 获取租户名
        /// </summary>
        public string TenantName { get; private set; }

        /// <summary>
        /// 获取租户显示名
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 获取租户的描述信息
        /// </summary>
        public string Description { get; private set; }

        #region ISoftDelete Members

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示否租户可以
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
        /// 获取创建时间
        /// </summary>
        public DateTime? UpdatedDate { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>RoleTenant</c>实例。仅供 Lazy 使用
        /// </summary>
        public Tenant()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>Tenant</c>实例
        /// </summary>
        /// <param name="tenantName">租户名称</param>
        /// <param name="displayName">租户显示名</param>
        /// <param name="description">租户描述</param>
        /// <param name="createdBy">创建人</param>
        public Tenant(string tenantName, string displayName, string description, string createdBy)
        {
            this.TenantName = tenantName;
            this.DisplayName = displayName;
            this.Description = description;
            this.CreatedBy = createdBy;
            this.CreatedDate = DateTime.Now;

            this.GenerateNewIdentity();
            this.Enable();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 更新租户信息
        /// </summary>
        /// <param name="displayName">租户显示名</param>
        /// <param name="description">租户描述</param>
        /// <param name="updatedBy">更新人</param>
        public void UpdateTenant(string displayName, string description, string updatedBy)
        {
            this.DisplayName = displayName;
            this.Description = description;
            this.UpdatedBy = updatedBy;
            this.UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// 启用租户
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
                this.Enabled = true;
        }

        /// <summary>
        /// 禁用租户
        /// </summary>
        /// <param name="disabledBy">禁用此租户的操作人</param>
        public void Disable(string disabledBy)
        {
            this.Disable();
            this.UpdatedBy = disabledBy;
            this.UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// 禁用租户
        /// </summary>
        public void Disable()
        {
            if (this.Enabled)
                this.Enabled = false;
        }

        #endregion
       
    }
}
