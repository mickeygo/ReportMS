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
        public MenuLevel Level { get; private set; }

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

        /// <summary>
        /// 初始化一个新的<c>Menu</c>实例。仅供 Lazy 使用
        /// </summary>
        public Menu()
        {
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
        public Menu(string menuName, string displayName, string description, Guid? parentId, MenuLevel level, int sort, Guid? actionsId, string createdBy)
        {
            this.MenuName = menuName;
            this.DisplayName = displayName;
            this.Description = description;
            this.ParentId = parentId;
            this.Level = level;
            this.Sort = sort;
            this.ActionsId = actionsId;
            this.CreatedBy = createdBy;
            this.CreatedDate = DateTime.Now;

            this.GenerateNewIdentity();
            this.Enable();
            this.AdjustParentViaLevel();
        }

        #endregion

        #region Public Methods

        /// <param name="displayName">菜单显示名</param>
        /// <param name="description">菜单描述</param>
        /// <param name="parentId">父菜单 ID</param>
        /// <param name="level">菜单目录级别, 0 级表示根目录， 1-m 级必须有 parentId</param>
        /// <param name="sort">菜单排序</param>
        /// <param name="actionsId">功能行为 ID</param>
        /// <param name="updatedBy">创建人</param>
        public void Update(string displayName, string description, Guid? parentId, MenuLevel level, int sort, Guid? actionsId, string updatedBy)
        {
            this.DisplayName = displayName;
            this.Description = description;
            this.ParentId = parentId;
            this.Level = level;
            this.Sort = sort;
            this.ActionsId = actionsId;

            this.SetUpdatedBy(updatedBy);
            this.AdjustParentViaLevel();
        }

        /// <summary>
        /// 设置更新人信息
        /// </summary>
        /// <param name="updatedBy">更新人</param>
        public void SetUpdatedBy(string updatedBy)
        {
            this.UpdatedBy = updatedBy;
            this.UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// Sort 自增加
        /// </summary>
        public void IncreaseSort()
        {
            this.Sort++;
        }

        /// <summary>
        /// Sort 自增减
        /// </summary>
        public void DecreaseSort()
        {
            this.Sort--;
        }

        /// <summary>
        /// 设置菜单排序
        /// </summary>
        /// <param name="sort">排序</param>
        public void SetSort(int sort)
        {
            this.Sort = sort;
        }

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

        #region Private Methods

        private void AdjustParentViaLevel()
        {
            // 当为菜单根目录时，ParentID 设为 null。Action 可以存在
            if (this.Level == MenuLevel.Parent)
                this.ParentId = null;
        }

        private bool ValidateLevel()
        {
            if (this.Level == MenuLevel.Children)
                return this.ParentId != null;

            return true;
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(this.MenuName))
                yield return new ValidationResult("The menu name is null or empty.");
            if (String.IsNullOrWhiteSpace(this.DisplayName))
                yield return new ValidationResult("The menu display name is null or empty.");
            if (!this.ValidateLevel())
                yield return new ValidationResult("The parent id must be exist, due to the menu level is children.");
        }

        #endregion
    }
}
