using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;

namespace ReportMS.Domain.Models.ReportModule.ReportGroupAggregate
{
    /// <summary>
    /// 分组明细中包含的字段
    /// </summary>
    public class ReportGroupItemField : Entity, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取报表分组 ID
        /// </summary>
        public Guid ReportGroupItemId { get; private set; }

        /// <summary>
        /// 获取报表分组项
        /// </summary>
        public virtual ReportGroupItem ReportGroupItem { get; private set; }

        /// <summary>
        /// 获取字段名称
        /// </summary>
        public string FieldName { get; private set; }

        #endregion

        #region Ctor

        private ReportGroupItemField()
        {
            this.GenerateNewIdentity(); 
        }

        /// <summary>
        /// 创建一个新的<c>ReportGroupItemField</c>实例
        /// </summary>
        /// <param name="reportGroupItemId">报表分组 ID</param>
        /// <param name="fieldName">字段名称</param>
        public ReportGroupItemField(Guid reportGroupItemId, string fieldName) 
            : this()
        {
            this.ReportGroupItemId = reportGroupItemId;
            this.FieldName = fieldName;
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ReportGroupItemId == Guid.Empty)
                yield return new ValidationResult("The report group item id is null or empty.");

            if (String.IsNullOrWhiteSpace(this.FieldName))
                yield return new ValidationResult("The field name is null or empty.");
        }
        
        #endregion
    }
}
