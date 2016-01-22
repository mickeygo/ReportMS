using System;
using System.Collections.Generic;
using System.Security.Claims;
using Gear.Infrastructure.Authentication;

namespace Gear.Infrastructure.Web.Authorization
{
    /// <summary>
    /// Claim 管理
    /// </summary>
    public class ClaimManager
    {
        #region Private Fields

        private readonly List<Claim> _claims = new List<Claim>();

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>ClaimManager</c>实例
        /// </summary>
        /// <param name="data">身份验证数据</param>
        public ClaimManager(AuthenticationData data)
        {
            this._claims.Add(new Claim(ClaimTypes.NameIdentifier, data.Identifer.ToString()));
            this._claims.Add(new Claim(ClaimTypes.Name, data.Name));

            if (!String.IsNullOrWhiteSpace(data.Email))
                this._claims.Add(new Claim(ClaimTypes.Email, data.Email));

            if (data.Roles != null)
            {
                foreach (var role in data.Roles)
                    this._claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        #endregion 

        #region Public Properties

        /// <summary>
        /// 获取 Claim 集合
        /// </summary>
        public IEnumerable<Claim> Claims
        {
            get { return this._claims; }
        }

        #endregion
    }
}
