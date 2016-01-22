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

        private Tenant()
        {
            this.GenerateNewIdentity();
            this.Enable();
        }

        /// <summary>
        /// 初始化一个新的<c>Tenant</c>实例
        /// </summary>
        /// <param name="tenantName">租户名称</param>
        /// <param name="displayName">租户显示名</param>
        /// <param name="description">租户描述</param>
        /// <param name="createdBy">创建人</param>
        /// <param name="createdDate">创建时间</param>
        public Tenant(string tenantName, string displayName, string description, string createdBy, DateTime? createdDate)
            : this()
        {
            this.TenantName = tenantName;
            this.DisplayName = displayName;
            this.Description = description;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
        }

        #endregion

        #region Public Methods

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
        public void Disable()
        {
            if (this.Enabled)
                this.Enabled = false;
        }

        #endregion
       
    }
}
