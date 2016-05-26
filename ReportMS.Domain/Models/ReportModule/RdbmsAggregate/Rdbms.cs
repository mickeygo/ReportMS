using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Gear.Infrastructure;
using Gear.Infrastructure.Algorithms.Cryptography;
using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Storage;

namespace ReportMS.Domain.Models.ReportModule.RdbmsAggregate
{
    /// <summary>
    /// 关系型数据库信息（聚合根）
    /// </summary>
    public class Rdbms : AggregateRoot, ISoftDelete, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// 获取关系型数据库名
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
        /// 获取一个<see cref="System.Boolean"/>值，表示数据库是否是只读
        /// </summary>
        public bool ReadOnly { get; private set; }

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
        public Rdbms()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>Rdbms</c>实例。
        /// 对 用户名 和 密码 会采用 AES 方式加密
        /// </summary>
        /// <param name="name">给定的名称</param>
        /// <param name="description">相关描述</param>
        /// <param name="server">服务器实例（数据源）</param>
        /// <param name="catalog">数据库实例</param>
        /// <param name="userId">用户</param>
        /// <param name="password">用户密码</param>
        /// <param name="readOnly">数据库是否是只读</param>
        /// <param name="provider">数据库提供者（SqlServer 或 Oracle, MySql 等）</param>
        public Rdbms(string name, string description, string server, string catalog, string userId, string password,
            bool readOnly, string provider)
        {
            this.Name = name;
            this.Description = description;
            this.Server = server;
            this.Catalog = catalog;
            this.UserId = userId;
            this.Password = password;
            this.ReadOnly = readOnly;
            this.Provider = provider;
            this.CreatedDate = DateTime.Now;

            this.GenerateNewIdentity();
            this.Enable();
            this.EncryptUserIdAndPwd();
        }

        /// <summary>
        /// 初始化一个新的<c>Rdbms</c>实例。
        /// 对 用户名 和 密码 会采用 AES 方式加密
        /// </summary>
        /// <param name="name">给定的名称</param>
        /// <param name="description">相关描述</param>
        /// <param name="server">服务器实例（数据源）</param>
        /// <param name="catalog">数据库实例</param>
        /// <param name="userId">用户</param>
        /// <param name="password">用户密码</param>
        /// <param name="readOnly">数据库是否是只读</param>
        /// <param name="rdbms"><see cref="RDBMS"/>关系型数据库</param>
        public Rdbms(string name, string description, string server, string catalog, string userId, string password,
            bool readOnly, RDBMS rdbms)
            : this(name, description, server, catalog, userId, password, readOnly, RdbmsProvider.GetRdbmsProvider(rdbms))
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 更新关系型数据库。
        /// 对 用户名 和 密码 会采用 AES 方式加密
        /// </summary>
        /// <param name="name">给定的名称</param>
        /// <param name="description">相关描述</param>
        /// <param name="server">服务器实例（数据源）</param>
        /// <param name="catalog">数据库实例</param>
        /// <param name="userId">用户</param>
        /// <param name="password">用户密码</param>
        /// <param name="readOnly">数据库是否是只读</param>
        /// <param name="provider">数据库提供者（SqlServer 或 Oracle, MySql 等）</param>
        public void Update(string name, string description, string server, string catalog, string userId, string password,
            bool readOnly, string provider)
        {
            this.Name = name;
            this.Description = description;
            this.Server = server;
            this.Catalog = catalog;
            this.UserId = userId;
            this.Password = password;
            this.ReadOnly = readOnly;
            this.Provider = provider;

            this.EncryptUserIdAndPwd();
        }

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
            var encryptedUserId = CryptoFactory.AES.Encrypt(this.UserId);
            var encryptedPwd = CryptoFactory.AES.Encrypt(this.Password);

            this.UserId = encryptedUserId;
            this.Password = encryptedPwd;
        }

        private bool CheckProvider()
        {
            if (String.IsNullOrWhiteSpace(this.Provider))
                return false;

            var rdbms = from int item in Enum.GetValues(typeof (RDBMS))
                select RdbmsProvider.GetRdbmsProvider((RDBMS) item);

            return rdbms.Contains(this.Provider);
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
            if (!this.CheckProvider())
                yield return new ValidationResult("The database provider is null or empty.");
        }

        #endregion
    }
}
