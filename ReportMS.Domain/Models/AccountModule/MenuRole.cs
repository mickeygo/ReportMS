using System;
using Gear.Infrastructure;

namespace ReportMS.Domain.Models.AccountModule
{
    /// <summary>
    /// 菜单所在的角色（聚合根）
    /// </summary>
    public class MenuRole : AggregateRoot
    {
        #region Properties

        /// <summary>
        /// 获取菜单 ID
        /// </summary>
        public Guid MenuId { get; private set; }

        /// <summary>
        /// 获取菜单
        /// </summary>
        public virtual Menu Menu { get; private set; }

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

        private MenuRole()
        {
            this.GenerateNewIdentity();
            this.CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// 初始化一个新的<c>MenuRole</c>实例
        /// </summary>
        /// <param name="menuId">菜单 ID</param>
        /// <param name="roleId">角色 ID</param>
        /// <param name="createdBy">创建人</param>
        public MenuRole(Guid menuId, Guid roleId, string createdBy)
            : this()
        {
            this.MenuId = menuId;
            this.RoleId = roleId;
            this.CreatedBy = createdBy;
        }

        #endregion
    }
}
