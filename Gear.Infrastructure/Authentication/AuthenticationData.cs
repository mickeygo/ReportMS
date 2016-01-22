using System;

namespace Gear.Infrastructure.Authentication
{
    /// <summary>
    /// 表示用来储存与身份的相关数据
    /// </summary>
    [Serializable]
    public sealed class AuthenticationData
    {
        #region Public Properties

        /// <summary>
        /// 获取用户身份标识
        /// </summary>
        public Guid? Identifer { get; private set; }

        /// <summary>
        /// 获取用户名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 获取用户邮件
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// 获取用户拥有的角色
        /// </summary>
        public string[] Roles { get; private set; }

        #endregion

        #region

        /// <summary>
        /// 初始化一个新的<c>AuthenticationOptions</c>实例
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="roles">用户拥有的角色集</param>
        public AuthenticationData(string name, params string[] roles)
            : this(null, name, roles)
        {
        }

        /// <summary>
        /// 初始化一个新的<c>AuthenticationOptions</c>实例
        /// </summary>
        /// <param name="identifer">用户身份标识</param>
        /// <param name="name">用户名</param>
        /// <param name="roles">用户拥有的角色集</param>
        public AuthenticationData(Guid? identifer, string name, params string[] roles)
            : this(identifer, name, null, roles)
        {
        }

        /// <summary>
        /// 初始化一个新的<c>AuthenticationOptions</c>实例
        /// </summary>
        /// <param name="identifer">用户身份标识</param>
        /// <param name="name">用户名</param>
        /// <param name="email">用户邮件</param>
        /// <param name="roles">用户拥有的角色集</param>
        public AuthenticationData(Guid? identifer, string name, string email, params string[] roles)
        {
            this.Identifer = identifer;
            this.Name = name;
            this.Email = email;
            this.Roles = roles;
        }

        #endregion
    }
}
