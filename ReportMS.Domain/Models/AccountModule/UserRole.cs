using System;
using Gear.Infrastructure;

namespace ReportMS.Domain.Models.AccountModule
{
    /// <summary>
    /// 用户拥有的角色（聚合根）
    /// </summary>
    public class UserRole : AggregateRoot
    {
        #region Properties

        /// <summary>
        /// 获取用户 ID
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// 获取用户
        /// </summary>
        public virtual User User { get; private set; }

        /// <summary>
        /// 获取角色 ID
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

        /// <summary>
        /// 初始化一个新的<c>UserRole</c>实例。仅供 Lazy 使用
        /// </summary>
        public UserRole()
        {
        }

        /// <summary>
        /// 创建一个新的<c>UserRole</c>实例
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roleId">角色 ID</param>
        /// <param name="createdBy">创建人</param>
        public UserRole(Guid userId, Guid roleId, string createdBy)
        {
            UserId = userId;
            RoleId = roleId;
            CreatedBy = createdBy;
            this.CreatedDate = DateTime.Now;

            this.GenerateNewIdentity();
        }

        #endregion
    }
}
