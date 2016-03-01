using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;
using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;

namespace ReportMS.Domain.Models.ReportModule.ReportProfileAggregate
{
    /// <summary>
    /// 报表配置（聚合根）.
    /// 用于对报表中的字段进行筛选后，归纳到报表分组中
    /// </summary>
    public class ReportProfile : AggregateRoot, ISoftDelete, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取报表配置名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 获取报表配置名称描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 获取报表 Id
        /// </summary>
        public Guid ReportId { get; private set; }

        /// <summary>
        /// 获取报表
        /// </summary>
        public virtual Report Report { get; private set; }

        /// <summary>
        /// 获取此数据创建人
        /// </summary>
        public string CreatedBy { get; private set; }

        /// <summary>
        /// 获取此数据创建日期
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 获取此数据更新人
        /// </summary>
        public string UpdatedBy { get; private set; }

        /// <summary>
        /// 获取此数据更新日期
        /// </summary>
        public DateTime? UpdatedDate { get; private set; }

        /// <summary>
        /// 获取报表配置字段集合信息
        /// </summary>
        public virtual ICollection<ReportProfileField> ReportProfileFields { get; private set; }

        #endregion

        #region ISoftDelete Members

        /// <summary>
        /// 获取此数据是否可用
        /// </summary>
        public bool Enabled { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>ReportProfile</c>实例. 仅供 Lazy 加载使用
        /// </summary>
        public ReportProfile()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>ReportProfile</c>实例
        /// </summary>
        /// <param name="name">报表配置名</param>
        /// <param name="description">报表配置描述</param>
        /// <param name="reportId">要配置的报表 Id</param>
        /// <param name="createdBy">创建人</param>
        public ReportProfile(string name, string description, Guid reportId, string createdBy)
        {
            Name = name;
            Description = description;
            ReportId = reportId;
            CreatedBy = createdBy;
            CreatedDate = DateTime.Now;

            this.GenerateNewIdentity();
            this.Enable();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 添加报表配置字段集合
        /// </summary>
        /// <param name="fields">要添加的报表配置字段集合</param>
        public void AddProfileFields(IEnumerable<ReportProfileField> fields)
        {
            this.EnsureNotNullOfProfileFields();
            foreach (var field in fields)
                this.ReportProfileFields.Add(field);
        }

        /// <summary>
        /// 添加报表配置字段
        /// </summary>
        /// <param name="field">要添加的报表配置字段</param>
        public void AddProfileField(ReportProfileField field)
        {
            this.EnsureNotNullOfProfileFields();
            this.ReportProfileFields.Add(field);
        }

        /// <summary>
        /// 更新报表配置头
        /// </summary>
        /// <param name="name">要更新报表配置名</param>
        /// <param name="description">要更新的报表配置描述</param>
        /// <param name="updatedBy">更新人</param>
        public void UpdateProfileHeader(string name, string description, string updatedBy)
        {
            this.Name = name;
            this.Description = description;
            this.UpdatedBy = UpdatedBy;
            this.UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// 启用报表配置
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
                this.Enabled = true;
        }

        /// <summary>
        /// 禁用报表配置
        /// </summary>
        public void Disable()
        {
            if (this.Enabled)
                this.Enabled = false;
        }

        #endregion

        #region Private Methods

        private void EnsureNotNullOfProfileFields()
        {
            if (this.ReportProfileFields == null)
                this.ReportProfileFields = new HashSet<ReportProfileField>();
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("The report profile name is null or empty.");
        }

        #endregion
    }
}
