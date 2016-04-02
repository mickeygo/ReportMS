using System;
using System.Collections.Generic;
using System.Data;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 储存容器提供者基类
    /// </summary>
    public abstract class StorageProvider
    {
        #region Properties

        /// <summary>
        /// 获数据源连接
        /// </summary>
        protected IDbConnection Connection { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 创建数据源连接, 用来指定不同的数据库
        /// </summary>
        /// <returns><see cref="System.Data.Common.DbConnection"/></returns>
        protected abstract IDbConnection CreateInternalConnection();

        /// <summary>
        /// 获取第一个对象，若没有，则返回为 null
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">查询参数</param>
        /// <returns><c>T</c></returns>
        public abstract T SelectFirstOrDefault<T>(string sqlQuery, object param = null);

        /// <summary>
        /// 获取在存储容器中的对象集合, 若没有数据，则返回空的集合（非 null）
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">查询参数</param>
        /// <returns><c>T</c>集合</returns>
        public abstract IEnumerable<T> Select<T>(string sqlQuery, object param = null);

        /// <summary>
        /// 获取在存储容器中的对象集合, 若没有数据，则返回空的集合（非 null）
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="start">分页起始数</param>
        /// <param name="count">每页的数量</param>
        /// <param name="param">查询参数</param>
        /// <returns><c>T</c>集合</returns>
        public abstract IEnumerable<T> Select<T>(string sqlQuery, int start, int count, object param = null);

        /// <summary>
        /// 获取存储容器中的对象数量
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">查询参数</param>
        /// <returns><see cref="System.Int32"/></returns>
        public abstract int GetRecordCount(string sqlQuery, object param = null);

        /// <summary>
        /// 获取 DataReader 对象
        /// 在关闭 DataReader 时，也会同时关闭对数据库的连接
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">查询参数</param>
        /// <returns><see cref="System.Data.Common.DbDataReader"/></returns>
        public abstract IDataReader GetDataReader(string sqlQuery, object param = null);

        /// <summary>
        /// 获取 DataReader 对象
        /// 在关闭 DataReader 时，也会同时关闭对数据库的连接
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="start">分页起始数</param>
        /// <param name="count">每页的数量</param>
        /// <param name="param">查询参数</param>
        /// <returns><see cref="System.Data.Common.DbDataReader"/></returns>
        public abstract IDataReader GetDataReader(string sqlQuery, int start, int count, object param = null);

        /// <summary>
        /// 获取数据源提供程序
        /// </summary>
        protected string ProviderName { get; private set; }

        /// <summary>
        /// 建制数据源连接
        /// </summary>
        /// <param name="connection">数据源连接</param>
        public void BuildConnection(StorageConfiguration connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            this.ProviderName = connection.ProviderName;
            this.Connection = this.CreateInternalConnection();
            this.Connection.ConnectionString = connection.ConnectionString;
        }

        #endregion
    }
}
