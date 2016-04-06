using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;
using Gear.Infrastructure.Repositories;

namespace ReportMS.Domain.Models.AccountModule
{
    /// <summary>
    /// Active Method，功能行为（聚合根）
    /// </summary>
    public class Actions : AggregateRoot, ISoftDelete, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取区域（MVC）
        /// </summary>
        public string Area { get; private set; }

        /// <summary>
        /// 获取控制器（MVC）
        /// </summary>
        public string Controller { get; private set; }

        /// <summary>
        /// 获取 Action（MVC）
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// 获取或设置描述
        /// </summary>
        public string Description { get; private set; }

        #region ISoftDelete Members

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示此 Action 是否可用
        /// </summary>
        public bool Enabled { get; private set; }

        #endregion

        /// <summary>
        /// 获取或设置创建人
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

        /// <summary>
        /// 初始化一个新的<c>Actions</c>对象实例。仅用 Layz 使用
        /// </summary>
        public Actions()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>Actions</c>对象实例
        /// </summary>
        /// <param name="area">区域(MVC)</param>
        /// <param name="controller">控制器(MVC)</param>
        /// <param name="action">Action(MVC)</param>
        /// <param name="description">描述</param>
        /// <param name="createdBy">创建人</param>
        public Actions(string area, string controller, string action, string description, string createdBy)
        {
            this.Area = area;
            this.Controller = controller;
            this.Action = action;
            this.Description = description;
            this.CreatedBy = createdBy;
            this.CreatedDate = DateTime.Now;

            this.GenerateNewIdentity();
            this.Enable();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 更新 Action 信息
        /// </summary>
        /// <param name="area">区域(MVC)</param>
        /// <param name="controller">控制器(MVC)</param>
        /// <param name="action">Action(MVC)</param>
        /// <param name="description">描述</param>
        /// <param name="updatedBy">更新人</param>
        public void Update(string area, string controller, string action, string description, string updatedBy)
        {
            this.Area = area;
            this.Controller = controller;
            this.Action = action;
            this.Description = description;

            this.SetUpdatedBy(updatedBy);
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
        /// 启用该 Action
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
                this.Enabled = true;
        }

        /// <summary>
        /// 禁用该 Action
        /// </summary>
        public void Disable()
        {
            if (this.Enabled)
                this.Enabled = false;
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(this.Controller))
                yield return new ValidationResult("The controller is null or empty.");
            if (String.IsNullOrWhiteSpace(this.Action))
                yield return new ValidationResult("The action is null or empty.");
        }

        #endregion
    }
}
