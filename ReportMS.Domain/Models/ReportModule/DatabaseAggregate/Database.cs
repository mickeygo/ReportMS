using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;
using Gear.Infrastructure.Algorithms.Cryptography;
using Gear.Infrastructure.Repositories;

namespace ReportMS.Domain.Models.ReportModule.DatabaseAggregate
{
    /// <summary>
    /// 数据库信息（聚合根）
    /// </summary>
    public class Database : AggregateRoot, ISoftDelete, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取连接的据库名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 获取相关描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 获取服务器实例（数据源）
        /// </summary>
        public string Server { get; private set; }

        /// <summary>
        /// 获取数据库实例
        /// </summary>
        public string Catalog { get; private set; }

        /// <summary>
        /// 获取用户（会加密）
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// 获取用户密码（会加密）
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 获取数据库提供者（SqlServer 或 Oracle）
        /// </summary>
        public string Provider { get; private set; }

        /// <summary>
        /// 获取此记录创建时间
        /// </summary>
        public DateTime? CreatedDate { get; private set; }

        #endregion

        #region ISoftDelete Members

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示是否此数据有效
        /// </summary>
        public bool Enabled { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>Database</c>实例。仅供 Lazy 加载使用
        /// </summary>
        public Database()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>Database</c>实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description">相关描述</param>
        /// <param name="server">服务器实例（数据源）</param>
        /// <param name="catalog">数据库实例</param>
        /// <param name="userId">用户</param>
        /// <param name="password">用户密码</param>
        /// <param name="provider">数据库提供者（SqlServer 或 Oracle, MySql 等）</param>
        public Database(string name, string description, string server, string catalog, string userId, string password,
            string provider)
        {
            Name = name;
            Description = description;
            Server = server;
            Catalog = catalog;
            UserId = userId;
            Password = password;
            Provider = provider;
            this.CreatedDate = DateTime.Now;

            this.GenerateNewIdentity();
            this.Enable();
            this.EncryptUserIdAndPwd();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 启用数据库
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
                this.Enabled = true;
        }

        /// <summary>
        /// 禁用数据库
        /// </summary>
        public void Disable()
        {
            if (this.Enabled)
                this.Enabled = false;
        }

        /// <summary>
        /// 解密数据库连接的用户名和密码
        /// </summary>
        public void DecryptUserIdAndPwd()
        {
            var decryptedUserId = CryptoFactory.AES.Decrypt(this.UserId);
            var decryptedPwd = CryptoFactory.AES.Decrypt(this.Password);

            this.UserId = decryptedUserId;
            this.Password = decryptedPwd;
        }

        #endregion

        #region Private Methods

        private void EncryptUserIdAndPwd()
        {
            var decryptedUserId = CryptoFactory.AES.Encrypt(this.UserId);
            var decryptedPwd = CryptoFactory.AES.Encrypt(this.Password);

            this.UserId = decryptedUserId;
            this.Password = decryptedPwd;
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("The database name is null or empty.");
            if (String.IsNullOrWhiteSpace(this.Server))
                yield return new ValidationResult("The database server is null or empty.");
            if (String.IsNullOrWhiteSpace(this.Catalog))
                yield return new ValidationResult("The database catalog is null or empty.");
            if (String.IsNullOrWhiteSpace(this.UserId))
                yield return new ValidationResult("The database user id is null or empty.");
            if (String.IsNullOrWhiteSpace(this.Password))
                yield return new ValidationResult("The database password is null or empty.");
            if (String.IsNullOrWhiteSpace(this.Provider))
                yield return new ValidationResult("The database provider is null or empty.");
        }

        #endregion
    }
}
