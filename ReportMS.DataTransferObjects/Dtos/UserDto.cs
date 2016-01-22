using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 用户 DTO 对象
    /// </summary>
    [DataContract]
    public class UserDto
    {
        /// <summary>
        /// 获取或设置用户 ID
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置工号
        /// </summary>
        [DataMember]
        public string EmployeeNo { get; set; }

        /// <summary>
        /// 获取或设置邮件
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置英文姓名
        /// </summary>
        [DataMember]
        public string EnglishName { get; set; }

        /// <summary>
        /// 获取或设置本名
        /// </summary>
        [DataMember]
        public string LocalName { get; set; }

        /// <summary>
        /// 获取或设置属于的公司
        /// </summary>
        [DataMember]
        public string Company { get; set; }

        /// <summary>
        /// 获取或设置属于的组织
        /// </summary>
        [DataMember]
        public string Organization { get; set; }

        /// <summary>
        /// 获取或设置属于的组织描述
        /// </summary>
        [DataMember]
        public string OrganizationDescription { get; set; }

        /// <summary>
        /// 获取或设置属于的部门
        /// </summary>
        [DataMember]
        public string Department { get; set; }

        /// <summary>
        /// 获取或设置工作职位
        /// </summary>
        [DataMember]
        public string Job { get; set; }

        /// <summary>
        /// 获取或设置电话
        /// </summary>
        [DataMember]
        public string Tel { get; set; }

        /// <summary>
        /// 获取或设置分机
        /// </summary>
        [DataMember]
        public string Extension { get; set; }

        /// <summary>
        /// 获取或设置 VOIP
        /// </summary>
        [DataMember]
        public string VOIP { get; set; }

        /// <summary>
        /// 获取或设置入职日期
        /// </summary>
        [DataMember]
        public DateTime? OnBoardDate { get; set; }

        /// <summary>
        /// 获取或设置上级管理者
        /// </summary>
        [DataMember]
        public string Manager { get; set; }

        /// <summary>
        /// 获取或设置代理人
        /// </summary>
        [DataMember]
        public string Agent { get; set; }

        /// <summary>
        /// 获取或设置职等
        /// </summary>
        [DataMember]
        public string Grade { get; set; }

        /// <summary>
        /// 获取或设置班别
        /// </summary>
        [DataMember]
        public string Shift { get; set; }

        /// <summary>
        /// 获取或设置创建人
        /// </summary>
        [DataMember]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [DataMember]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 获取或设置更新人
        /// </summary>
        [DataMember]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 获取或设置更新时间
        /// </summary>
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
    }
}
