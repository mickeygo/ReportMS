using System;
using System.Data;
using Gear.Infrastructure.Storage.Config;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 储存容器提供者基类
    /// </summary>
    public abstract class StorageProvider
    {
        private IDbConnection _connection;

        #region Properties

        /// <summary>
        /// 获数据源连接
        /// </summary>
        public IDbConnection Connection
        {
            get { return this._connection ?? (this._connection = this.CreateInternalConnection()); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 创建数据源连接, 用来指定不同的数据库
        /// </summary>
        /// <returns><see cref="System.Data.Common.DbConnection"/></returns>
        protected abstract IDbConnection CreateInternalConnection();

        /// <summary>
        /// 获取数据源提供程序
        /// </summary>
        protected string ProviderName { get; private set; }

        /// <summary>
        /// 建制数据源连接
        /// </summary>
        /// <param name="connection">数据源连接</param>
        public void BuildConnection(ConnectionConfig connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            this.ProviderName = connection.ProviderName;  // 供 Factory 使用
            this.Connection.ConnectionString = connection.ConnectionString;
        }

        #endregion
    }
}
