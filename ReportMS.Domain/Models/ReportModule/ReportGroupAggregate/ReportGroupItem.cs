using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;
using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;

namespace ReportMS.Domain.Models.ReportModule.ReportGroupAggregate
{
    /// <summary>
    /// 报表分组的详细信息
    /// </summary>
    public class ReportGroupItem : AggregateRoot, ISoftDelete, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取报表分组的 ID
        /// </summary>
        public Guid ReportGroupId { get; private set; }

        /// <summary>
        /// 获取报表分组
        /// </summary>
        public virtual ReportGroup ReportGroup { get; private set; }

        /// <summary>
        /// 获取报表 ID
        /// </summary>
        public Guid ReportId { get; private set; }

        /// <summary>
        /// 获取报表
        /// </summary>
        public virtual Report Report { get; private set; }

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
        /// 获取报表组项字段集合
        /// </summary>
        public virtual ICollection<ReportGroupItemField> ReportGroupItemFields { get; private set; }

        #endregion

        #region Ctor

        private ReportGroupItem()
        {
            this.GenerateNewIdentity();
            this.Enable();

            this.CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// 创建一个新的<c>ReportGroupDetail</c>实例
        /// </summary>
        /// <param name="reportGroupId">报表分组的 ID</param>
        /// <param name="reportId">报表 ID</param>
        /// <param name="createdBy">创建人</param>
        public ReportGroupItem(Guid reportGroupId, Guid reportId, string createdBy)
            : this()
        {
            this.ReportGroupId = reportGroupId;
            this.ReportId = reportId;
            this.CreatedBy = createdBy;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 启用报表组项
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
                this.Enabled = true;
        }

        /// <summary>
        /// 禁用报表组项
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
            if (this.ReportGroupId == Guid.Empty)
                yield return new ValidationResult("The report group id is empty.");

            if (this.ReportId == Guid.Empty)
                yield return new ValidationResult("The report id is empty.");
        }

        #endregion
    }
}
