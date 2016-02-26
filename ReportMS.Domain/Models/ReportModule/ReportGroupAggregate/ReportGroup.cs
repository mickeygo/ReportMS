using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;
using Gear.Infrastructure.Repositories;

namespace ReportMS.Domain.Models.ReportModule.ReportGroupAggregate
{
    /// <summary>
    /// 报表分组, 聚合根
    /// </summary>
    public class ReportGroup : AggregateRoot, ISoftDelete, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取报表组的名称
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// 获取报表组的显示名称
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 获取报表组的描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值,表示报表组是否可用
        /// </summary>
        public bool Enabled { get; private set; }

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

        /// <summary>
        /// 获取报表分组的详细信息
        /// </summary>
        public virtual ICollection<ReportGroupItem> ReportGroupItems { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>ReportGroup</c>实例。仅供 Lazy 使用
        /// </summary>
        public ReportGroup()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>ReportGroup</c>实例
        /// </summary>
        /// <param name="name">报表组的名称</param>
        /// <param name="displayName">报表组的显示名称</param>
        /// <param name="description">报表组的描述</param>
        /// <param name="createdBy">创建人</param>
        public ReportGroup(string name, string displayName, string description, string createdBy)
        {
            this.GroupName = name;
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
        /// 添加报表分组项集合
        /// </summary>
        /// <param name="items">要添加的报表分组项集合</param>
        public void AddGroupItems(IEnumerable<ReportGroupItem> items)
        {
            this.EnsureNotNullOfReportGroupItems();
            this.EnsureParenetIdOfReportGroupItems(items);
            ((List<ReportGroupItem>)this.ReportGroupItems).AddRange(items);
        }

        /// <summary>
        /// 添加报表分组项
        /// </summary>
        /// <param name="item">要添加的报表分组项</param>
        public void AddGroupItem(ReportGroupItem item)
        {
            this.EnsureNotNullOfReportGroupItems();
            this.EnsureParenetIdOfReportGroupItem(item);
            this.ReportGroupItems.Add(item);
        }

        /// <summary>
        /// 启用报表组
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
                this.Enabled = true;
        }

        /// <summary>
        /// 禁用报表组
        /// </summary>
        public void Disable()
        {
            if (this.Enabled)
                this.Enabled = false;
        }

        #endregion

        #region Private Methods

        private void EnsureNotNullOfReportGroupItems()
        {
            if (this.ReportGroupItems == null)
                this.ReportGroupItems = new List<ReportGroupItem>();
        }

        private void EnsureParenetIdOfReportGroupItems(IEnumerable<ReportGroupItem> items)
        {
            foreach (var item in items)
                this.EnsureParenetIdOfReportGroupItem(item);
        }

        private void EnsureParenetIdOfReportGroupItem(ReportGroupItem item)
        {
            if (item.ReportGroupId != this.ID)
                item.AttachToParent(this.ID);
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(this.GroupName))
                yield return new ValidationResult("The report group name is null or empty.");

            if (String.IsNullOrWhiteSpace(this.DisplayName))
                yield return new ValidationResult("The report group display name is null or empty.");
        }

        #endregion
    }
}
