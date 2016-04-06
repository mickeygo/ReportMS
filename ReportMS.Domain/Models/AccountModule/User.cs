using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;
using Gear.Infrastructure.Repositories;

namespace ReportMS.Domain.Models.AccountModule
{
    /// <summary>
    /// 用户（聚合根）
    /// </summary>
    public class User : AggregateRoot, ISoftDelete, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取用户名
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// 获取密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 获取工号
        /// </summary>
        public string EmployeeNo { get; private set; }

        /// <summary>
        /// 获取邮件
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// 获取英文姓名
        /// </summary>
        public string EnglishName { get; private set; }

        /// <summary>
        /// 获取本名
        /// </summary>
        public string LocalName { get; private set; }

        /// <summary>
        /// 获取属于的公司
        /// </summary>
        public string Company { get; private set; }

        /// <summary>
        /// 获取属于的组织
        /// </summary>
        public string Organization { get; private set; }

        /// <summary>
        /// 获取属于的组织描述
        /// </summary>
        public string OrganizationDescription { get; private set; }

        /// <summary>
        /// 获取属于的部门
        /// </summary>
        public string Department { get; private set; }

        /// <summary>
        /// 获取工作职位
        /// </summary>
        public string Job { get; private set; }

        /// <summary>
        /// 获取电话
        /// </summary>
        public string Tel { get; private set; }

        /// <summary>
        /// 获取分机
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// 获取 VOIP
        /// </summary>
        public string VOIP { get; private set; }

        /// <summary>
        /// 获取入职日期
        /// </summary>
        public DateTime? OnBoardDate { get; private set; }

        /// <summary>
        /// 获取上级管理者
        /// </summary>
        public string Manager { get; private set; }

        /// <summary>
        /// 获取代理人
        /// </summary>
        public string Agent { get; private set; }

        /// <summary>
        /// 获取职等
        /// </summary>
        public string Grade { get; private set; }

        /// <summary>
        /// 获取班别
        /// </summary>
        public string Shift { get; private set; }

        /// <summary>
        /// 获取是否可用
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
        /// 获取更新时间
        /// </summary>
        public DateTime? UpdatedDate { get; private set; }

        #endregion

        /// <summary>
        /// 初始化一个<c>User</c>对象，仅供 Lazy 使用
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// 创建一个新的<c>User</c>实例
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="employeeNo">工号</param>
        /// <param name="email">邮件</param>
        /// <param name="englishName">英文名</param>
        /// <param name="localName">本地名</param>
        /// <param name="company">公司名</param>
        /// <param name="organization">组织</param>
        /// <param name="organizationDescription">组织描述</param>
        /// <param name="department">部门</param>
        /// <param name="job">职位</param>
        /// <param name="tel">联系电话</param>
        /// <param name="extension">分机</param>
        /// <param name="voip">VOIP</param>
        /// <param name="onBoardDate">入职日期</param>
        /// <param name="manager">管理者</param>
        /// <param name="agent">代理人</param>
        /// <param name="grade">职等</param>
        /// <param name="shift">班别</param>
        /// <param name="createdBy">此记录创建人</param>
        public User(string userName, string password, string employeeNo, string email, string englishName,
            string localName, string company, string organization, string organizationDescription, string department,
            string job, string tel, string extension, string voip, DateTime? onBoardDate, string manager,
            string agent, string grade, string shift, string createdBy)
        {
            this.UserName = userName;
            this.Password = password;
            this.EmployeeNo = employeeNo;
            this.Email = email;
            this.EnglishName = englishName;
            this.LocalName = localName;
            this.Company = company;
            this.Organization = organization;
            this.OrganizationDescription = organizationDescription;
            this.Department = department;
            this.Job = job;
            this.Tel = tel;
            this.Extension = extension;
            this.VOIP = voip;
            this.OnBoardDate = onBoardDate;
            this.Manager = manager;
            this.Agent = agent;
            this.Grade = grade;
            this.Shift = shift;
            this.CreatedBy = createdBy;

            this.GenerateNewIdentity();
            this.Enable();
        }

        #region

        /// <summary>
        /// 启用此用户
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
                this.Enabled = true;
        }

        /// <summary>
        /// 禁用此用户
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
            if (String.IsNullOrWhiteSpace(this.UserName))
                yield return new ValidationResult("The user name is null or empty.");
        }

        #endregion
    }
}
