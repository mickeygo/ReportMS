using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;
using Gear.Infrastructure.Repositories;

namespace ReportMS.Domain.Models.AccountModule
{
    /// <summary>
    /// 菜单（聚合根）
    /// </summary>
    public class Menu : AggregateRoot, ISoftDelete, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取菜单名
        /// </summary>
        public string MenuName { get; private set; }

        /// <summary>
        /// 获取菜单显示名
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 获取菜单描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 获取父级菜单 ID
        /// </summary>
        public Guid? ParentId { get; private set; }

        /// <summary>
        /// 获取菜单目录级别
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// 获取菜单的排序
        /// </summary>
        public int Sort { get; private set; }

        /// <summary>
        /// 获取功能行为 ID
        /// </summary>
        public Guid? ActionsId { get; private set; }

        /// <summary>
        /// 获取功能行为
        /// </summary>
        public virtual Actions Actions { get; private set; }

        #region ISoftDelete Members

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示此菜单是否可用
        /// </summary>
        public bool Enabled { get; private set; }

        #endregion

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
        /// 获取更新时间
        /// </summary>
        public DateTime? UpdatedDate { get; private set; }

        #endregion

        #region Ctor

        private Menu()
        {
            this.GenerateNewIdentity();
            this.Enable();

            this.CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// 初始化一个新的<c>Menu</c>实例
        /// </summary>
        /// <param name="menuName">菜单名</param>
        /// <param name="displayName">菜单显示名</param>
        /// <param name="description">菜单描述</param>
        /// <param name="parentId">父菜单 ID</param>
        /// <param name="level">菜单目录级别</param>
        /// <param name="sort">菜单排序</param>
        /// <param name="actionsId">功能行为 ID</param>
        /// <param name="createdBy">创建人</param>
        public Menu(string menuName, string displayName, string description, Guid? parentId, int level, int sort, Guid? actionsId, string createdBy)
            : this()
        {
            this.MenuName = menuName;
            this.DisplayName = displayName;
            this.Description = description;
            this.ParentId = parentId;
            this.Level = level;
            this.Sort = sort;
            this.ActionsId = actionsId;
            this.CreatedBy = createdBy;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 启用猜测
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
                this.Enabled = true;
        }

        /// <summary>
        /// 禁用菜单
        /// </summary>
        public void Disabled()
        {
            if (this.Enabled)
                this.Enabled = false;
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(this.MenuName))
                yield return new ValidationResult("The menu name is null or empty.");

            if (String.IsNullOrWhiteSpace(this.DisplayName))
                yield return new ValidationResult("The menu display name is null or empty.");
        }

        #endregion
    }
}
