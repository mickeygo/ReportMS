using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Models.ReportModule.ReportGroupAggregate
{
    /// <summary>
    /// 报表组角色（聚合根）
    /// </summary>
    public class ReportGroupRole : AggregateRoot, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取报表组 Id
        /// </summary>
        public Guid ReportGroupId { get; private set; }

        /// <summary>
        /// 获取报表组
        /// </summary>
        public virtual ReportGroup ReportGroup { get; private set; }

        /// <summary>
        /// 获取角色 Id
        /// </summary>
        public Guid RoleId { get; private set; }

        /// <summary>
        /// 获取角色
        /// </summary>
        public virtual Role Role { get; private set; }

        /// <summary>
        /// 获取创建人
        /// </summary>
        public string CreatedBy { get; private set; }

        /// <summary>
        /// 获取创建时间
        /// </summary>
        public DateTime? CreatedDate { get; private set; }

        #endregion

        #region Ctor

        private ReportGroupRole()
        {
            this.GenerateNewIdentity();
            this.CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// 创建一个新的<c>ReportGroupRole</c>实例
        /// </summary>
        /// <param name="reportGroupId">报表组 Id</param>
        /// <param name="roleId">角色 Id</param>
        /// <param name="createdBy">创建人</param>
        public ReportGroupRole(Guid reportGroupId, Guid roleId, string createdBy)
            : this()
        {
            this.ReportGroupId = reportGroupId;
            this.RoleId = roleId;
            this.CreatedBy = createdBy;
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ReportGroupId == Guid.Empty)
                yield return new ValidationResult("The report group id is empty.");
            
            if (this.RoleId == Guid.Empty)
                yield return new ValidationResult("The role id is empty.");
        }

        #endregion
    }
}
