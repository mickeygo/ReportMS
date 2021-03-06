﻿using System;
using Gear.Infrastructure.Algorithms.Cryptography;
using Gear.Infrastructure.Web.Utility;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Web.Client.MembershipWebService;

namespace ReportMS.Web.Client.Membership
{
    /// <summary>
    /// 用户信息验证程序。
    /// 对登录用户进行本地验证或远程验证
    /// </summary>
    public class UserValidator
    {
        #region Private Fields

        private const string SiteId = "ReportMS";
        private readonly string _userName;
        private readonly string _password;

        #endregion

        #region Ctor

        /// <summary>
        /// 创建一个新的<c>UserValidator</c>实例
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码(原始密码)</param>
        public UserValidator(string userName, string password)
        {
            this._userName = userName;
            this._password = password;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 用本地数据验证用户信息
        /// </summary>
        /// <returns>true 表示验证成功；false 表示验证失败</returns>
        public bool ValidateInLocal()
        {
            if (!this.CheckNotNullOfNameAndPwd())
                return false;

            var userName = this._userName.ToLower();  // To lower
            var user = this.GetUserInfo(userName);
            if (user == null)
                return false;

            var encryptedPwd = this.EncryptPassword(userName, this._password);
            return encryptedPwd.Equals(user.Password, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 通过 RPC 远程调用数据验证用户信息
        /// </summary>
        /// <returns>true 表示验证成功；false 表示验证失败</returns>
        public bool ValidateInRemote()
        {
            if (!this.CheckNotNullOfNameAndPwd())
                return false;

            var userName = this._userName.ToLower();  // To lower
            return this.ValidateUserMembershipRPC(userName, this._password, HttpRequestHelper.GetClientHostIP());
        }

        #endregion

        #region Private Methods

        private UserDto GetUserInfo(string usernMane)
        {
            return UserManager.Instance.GetUserInfo(usernMane);
        }

        private bool ValidateUserMembershipRPC(string userName, string password, string localId)
        {
            MembershipWebserviceSoapClient client = null;
            try
            {
                client = new MembershipWebserviceSoapClient();
                var siteId = SiteId;
                var result = client.login(userName, password, siteId, localId);
                return !String.IsNullOrEmpty(result);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }

        private string EncryptPassword(string userName, string password)
        {
            return CryptoFactory.MD5.Encrypt(userName + password);
        }

        private bool CheckNotNullOfNameAndPwd()
        {
            return !String.IsNullOrWhiteSpace(this._userName) && !String.IsNullOrWhiteSpace(this._password);
        }

        #endregion
    }
}
