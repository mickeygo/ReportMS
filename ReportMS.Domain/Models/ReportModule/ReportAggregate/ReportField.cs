using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Gear.Infrastructure;

namespace ReportMS.Domain.Models.ReportModule.ReportAggregate
{
    /// <summary>
    /// 报表字段
    /// </summary>
    public class ReportField : Entity, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取报表 ID
        /// </summary>
        public Guid ReportId { get; private set; }

        /// <summary>
        /// 获取报表
        /// </summary>
        public virtual Report Report { get; private set; }

        /// <summary>
        /// 获取字段名
        /// </summary>
        public string FieldName { get; private set; }

        /// <summary>
        /// 获取字段显示名
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 获取字段的数据类型
        /// </summary>
        public string DataType { get; private set; }

        /// <summary>
        /// 获取字段排序
        /// </summary>
        public int Sort { get; private set; }

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

        private ReportField()
        {
            this.GenerateNewIdentity();
            this.CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// 创建一个新的<c>ReportField</c>实例对象
        /// </summary>
        /// <param name="reportId">报表ID</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="displayName">字段显示名</param>
        /// <param name="dataType">字段数据类型</param>
        /// <param name="sort">字段排序</param>
        /// <param name="createdBy">创建人</param>
        public ReportField(Guid reportId, string fieldName, string displayName, string dataType, int sort, string createdBy)
            : this()
        {
            this.ReportId = reportId;
            this.FieldName = fieldName;
            this.DisplayName = displayName;
            this.DataType = dataType;
            this.Sort = sort;
            this.CreatedBy = createdBy;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 验证字段名是否有效
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>ture 表示验证成功;false 表示验证失败</returns>
        public bool ValidateFieldName(string fieldName)
        {
            return fieldName != null && Regex.IsMatch(fieldName, @"^[a-zA-Z0-9_]+$");
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ReportId == Guid.Empty)
                yield return new ValidationResult("The report id is empty.");

            if (!this.ValidateFieldName(this.FieldName))
                yield return new ValidationResult("The report field is invalid.");
        }

        #endregion
    }
}
