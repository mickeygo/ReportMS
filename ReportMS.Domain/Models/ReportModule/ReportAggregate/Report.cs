using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Gear.Infrastructure;
using Gear.Infrastructure.Repositories;

namespace ReportMS.Domain.Models.ReportModule.ReportAggregate
{
    /// <summary>
    /// 报表（聚合根）
    /// </summary>
    public class Report : AggregateRoot, ISoftDelete, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取报表名
        /// </summary>
        public string ReportName { get; private set; }

        /// <summary>
        /// 获取报表显示名
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 获取描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 获取报表所在的数据库，值对象
        /// </summary>
        public Database Database { get; private set; }

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
        /// 获取字段集合
        /// </summary>
        public virtual ICollection<ReportField> Fields { get; private set; }

        #endregion

        #region Ctor

        private Report()
        {
            this.GenerateNewIdentity();
            this.Enable();

            this.CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// 创建<c>Report</c>对象
        /// </summary>
        /// <param name="reportName">报表名</param>
        /// <param name="displayName">报表显示名</param>
        /// <param name="description">报表描述</param>
        /// <param name="dbName">报表所在 DB</param>
        /// <param name="schema">报表所在 DB 的 schema</param>
        /// <param name="createdBy">创建人</param>
        public Report(string reportName, string displayName, string description, string dbName, string schema, string createdBy)
            : this(reportName, displayName, description, null, createdBy)
        {
            this.Database = new Database(dbName, schema);
        }

        /// <summary>
        /// 创建<c>Report</c>对象
        /// </summary>
        /// <param name="reportName">报表名</param>
        /// <param name="displayName">报表显示名</param>
        /// <param name="description">报表描述</param>
        /// <param name="database">报表所在 DB</param>
        /// <param name="createdBy">创建人</param>
        public Report(string reportName, string displayName, string description, Database database, string createdBy)
            : this()
        {
            this.ReportName = reportName;
            this.DisplayName = displayName;
            this.Description = description;
            this.Database = database;
            this.CreatedBy = createdBy;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 启用报表
        /// </summary>
        public void Enable()
        {
            this.Enabled = true;
        }

        /// <summary>
        /// 禁用报表
        /// </summary>
        public void Disable()
        {
            this.Enabled = false;
        }

        /// <summary>
        /// 验证报表名是否有效
        /// </summary>
        /// <param name="reportName">报表名称</param>
        /// <returns>ture 表示验证成功;false 表示验证失败</returns>
        public bool ValidateReportName(string reportName)
        {
            return reportName != null && Regex.IsMatch(reportName, @"^[a-zA-Z0-9_]+$");
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(this.ReportName))
                yield return new ValidationResult("The report name is null or empty.");

            if (String.IsNullOrWhiteSpace(this.DisplayName))
                yield return new ValidationResult("The report display name is null or empty.");

            var hasDatabaseValue = this.Database != null &&
                                   !String.IsNullOrWhiteSpace(this.Database.Name) &&
                                   !String.IsNullOrWhiteSpace(this.Database.Name);
            if (!hasDatabaseValue)
                yield return new ValidationResult("The report database value is null or empty.");

            if (!this.ValidateReportName(this.ReportName))
                yield return new ValidationResult("The report name is invalid.");
        }

        #endregion
    }
}
