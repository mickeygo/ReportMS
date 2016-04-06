using System;
using Gear.Infrastructure;

namespace ReportMS.Domain.Models.AccountModule
{
    /// <summary>
    /// 角色中的 Action（聚合根）
    /// </summary>
    public class ActionRole : AggregateRoot
    {
        #region Properties

        /// <summary>
        /// 获取 Action Id
        /// </summary>
        public Guid ActionsId { get; private set; }

        /// <summary>
        /// 获取 Action
        /// </summary>
        public virtual Actions Actions { get; private set; }

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

        #region ctor

        /// <summary>
        ///  初始化一个新的<c>RoleActionRole</c>实例。仅供 Lazy 使用
        /// </summary>
        public ActionRole()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>ActionRole</c>实例
        /// </summary>
        /// <param name="actionsId">Action Id</param>
        /// <param name="roleId">角色 Id</param>
        /// <param name="createdBy">创建人</param>
        public ActionRole(Guid actionsId, Guid roleId, string createdBy)
        {
            this.ActionsId = actionsId;
            this.RoleId = roleId;
            this.CreatedBy = createdBy;
            this.CreatedDate = DateTime.Now;

            this.GenerateNewIdentity();
        }

        #endregion
    }
}
