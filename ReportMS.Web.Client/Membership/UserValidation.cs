using System;
using Gear.Infrastructure.Algorithms.Cryptography;
using Gear.Infrastructure.Web.Utility;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Web.Client.MembershipWebService;

namespace ReportMS.Web.Client.Membership
{
    /// <summary>
    /// 用户信息验证
    /// </summary>
    public class UserValidation
    {
        #region Private Fields

        private readonly string _userName;
        private readonly string _password;

        #endregion

        #region Ctor

        /// <summary>
        /// 创建一个新的<c>UserValidation</c>实例
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码(原始密码)</param>
        public UserValidation(string userName, string password)
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
            if (!CheckNotNullOfNameAndPwd())
                return false;

            var userName = this._userName.ToLower();
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
        public bool ValidateInRPC()
        {
            if (!CheckNotNullOfNameAndPwd())
                return false;

            var userName = this._userName.ToLower();
            return this.ValidateUseMembershipRPC(userName, this._password, HttpRequestHelper.GetClientHostIP());
        }

        #endregion

        #region Private Methods

        private UserDto GetUserInfo(string usernMane)
        {
            return UserManager.Instance.GetUserInfo(usernMane);
        }

        private bool ValidateUseMembershipRPC(string userName, string password, string localId)
        {
            MembershipWebserviceSoapClient client = null;
            try
            {
                client = new MembershipWebserviceSoapClient();
                var siteId = "ReportMS";
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
            var encryptedPwd = MD5Crypto.Encrypt(userName + password);
            return encryptedPwd;
        }

        private bool CheckNotNullOfNameAndPwd()
        {
            return !String.IsNullOrWhiteSpace(this._userName) && !String.IsNullOrWhiteSpace(this._password);
        }

        #endregion
    }
}
