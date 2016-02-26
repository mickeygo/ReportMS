using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;
using ReportMS.Domain.Models.ReportModule.ReportProfileAggregate;

namespace ReportMS.Domain.Models.ReportModule.ReportGroupAggregate
{
    /// <summary>
    /// 报表分组的详细信息
    /// </summary>
    public class ReportGroupItem : AggregateRoot, IValidatableObject
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
        /// 获取报表配置 Id
        /// </summary>
        public Guid ReportProfileId { get; private set; }

        /// <summary>
        /// 获取报表配置
        /// </summary>
        public virtual ReportProfile ReportProfile { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>ReportGroupItem</c>实例，仅供 Lazy 使用
        /// </summary>
        public ReportGroupItem()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>ReportGroupItem</c>实例
        /// </summary>
        /// <param name="reportGroupId">报表分组的 Id</param>
        /// <param name="reportProfileId">报表配置 Id</param>
        public ReportGroupItem(Guid reportGroupId, Guid reportProfileId)
        {
            this.ReportGroupId = reportGroupId;
            this.ReportProfileId = reportProfileId;

            this.GenerateNewIdentity();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 附加到父数据中
        /// </summary>
        /// <param name="parentId">要附加的父数据 Id</param>
        public void AttachToParent(Guid parentId)
        {
            this.ReportGroupId = parentId;
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ReportGroupId == Guid.Empty)
                yield return new ValidationResult("The report group id is empty.");

            if (this.ReportProfileId == Guid.Empty)
                yield return new ValidationResult("The report profile id is empty.");
        }

        #endregion
    }
}
