using System.Collections.Generic;
using System.Data;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 储存器管理类
    /// Remark：目前只用来做数据查询
    /// </summary>
    public sealed class StorageManager : IStorage
    {
        #region Private Fields

        private readonly IStorageProvider storageProvider;

        #endregion

        #region Ctor

        private StorageManager()
        {
            this.storageProvider = ServiceLocator.Instance.Resolve<IStorageProvider>();
        }

        private StorageManager(string connectionName)
            : this()
        {
            var config = new StorageConfiguration(connectionName);
            var connectionString = config.ToString();
            this.storageProvider.BuildConnection(connectionString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 创建容器管理实例
        /// </summary>
        /// <param name="connectionName">App.Config / Web.Config 文件中的 ConnectionStrings 连接名</param>
        /// <returns></returns>
        public static IStorage CreateInstance(string connectionName)
        {
            return new StorageManager(connectionName);
        }

        #endregion

        #region Interface IStorage Members

        /// <summary>
        /// 获取第一个对象，若没有，则返回为 null
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new Dictionary { { "Name", "gang.yang" }, { "Enabled", true } } 
        /// /// </param>
        /// <returns><c>T</c></returns>
        public T SelectFirstOrDefault<T>(string sqlQuery, IDictionary<string, object> param)
        {
            return this.storageProvider.SelectFirstOrDefault<T>(sqlQuery, param);
        }

        /// <summary>
        /// 获取第一个对象，若没有，则返回为 null
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new { Name = "gang.yang", Enabled = true }
        /// </param>
        /// <returns><c>T</c></returns>
        public T SelectFirstOrDefault<T>(string sqlQuery, object param = null)
        {
            return this.storageProvider.SelectFirstOrDefault<T>(sqlQuery, param);
        }

        /// <summary>
        /// 获取在存储容器中的对象集合, 若没有数据，则返回空的集合（非 null）
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new Dictionary { { "Name", "gang.yang" }, { "Enabled", true } } 
        /// /// </param>
        /// <returns><c>T</c>集合</returns>
        public IEnumerable<T> Select<T>(string sqlQuery, IDictionary<string, object> param)
        {
            return this.storageProvider.Select<T>(sqlQuery, param);
        }

        /// <summary>
        /// 获取在存储容器中的对象集合, 若没有数据，则返回空的集合（非 null）
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="start">分页起始数</param>
        /// <param name="length">每页的数量</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new Dictionary { { "Name", "gang.yang" }, { "Enabled", true } } 
        /// </param>
        /// <returns><c>T</c>集合</returns>
        public IEnumerable<T> Select<T>(string sqlQuery, int start, int length, IDictionary<string, object> param)
        {
            return this.storageProvider.Select<T>(sqlQuery, start, length, param);
        }

        /// <summary>
        /// 获取在存储容器中的对象集合, 若没有数据，则返回空的集合（非 null）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new { Name = "gang.yang", Enabled = true }
        /// </param>
        /// <returns><c>T</c>集合</returns>
        public IEnumerable<T> Select<T>(string sqlQuery, object param = null)
        {
            return this.storageProvider.Select<T>(sqlQuery, param);
        }

        /// <summary>
        /// 获取在存储容器中的对象集合, 若没有数据，则返回空的集合（非 null）
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="start">分页起始数</param>
        /// <param name="length">每页的数量</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new { Name = "gang.yang", Enabled = true }
        /// </param>
        /// <returns><c>T</c>集合</returns>
        public IEnumerable<T> Select<T>(string sqlQuery, int start, int length, object param = null)
        {
            return this.storageProvider.Select<T>(sqlQuery, start, length, param);
        }

        /// <summary>
        /// 获取存储容器中的对象数量
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new Dictionary { { "Name", "gang.yang" }, { "Enabled", true } } 
        /// </param>
        /// <returns><see cref="System.Int32"/></returns>
        public int GetRecordCount(string sqlQuery, IDictionary<string, object> param)
        {
            return this.storageProvider.GetRecordCount(sqlQuery, param);
        }

        /// <summary>
        /// 获取存储容器中的对象数量
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new { Name = "gang.yang", Enabled = true }
        /// </param>
        /// <returns><see cref="System.Int32"/></returns>
        public int GetRecordCount(string sqlQuery, object param = null)
        {
            return this.storageProvider.GetRecordCount(sqlQuery, param);
        }

        /// <summary>
        /// 获取 DataReader 对象
        /// 在关闭 DataReader 时，也会同时关闭对数据库的连接
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new Dictionary { { "Name", "gang.yang" }, { "Enabled", true } } 
        /// </param>
        /// <returns><see cref="System.Data.Common.DbDataReader"/></returns>
        public IDataReader GetDataReader(string sqlQuery, IDictionary<string, object> param)
        {
            return this.storageProvider.GetDataReader(sqlQuery, param);
        }

        /// <summary>
        /// 获取 DataReader 对象
        /// 在关闭 DataReader 时，也会同时关闭对数据库的连接
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="start">分页起始数</param>
        /// <param name="length">每页的数量</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new Dictionary { { "Name", "gang.yang" }, { "Enabled", true } } 
        /// </param>
        /// <returns><see cref="System.Data.Common.DbDataReader"/></returns>
        public IDataReader GetDataReader(string sqlQuery, int start, int length, IDictionary<string, object> param)
        {
            return this.storageProvider.GetDataReader(sqlQuery, start, length, param);
        }

        /// <summary>
        /// 获取 DataReader 对象
        /// 在关闭 DataReader 时，也会同时关闭对数据库的连接
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new { Name = "gang.yang", Enabled = true }
        /// </param>
        /// <returns><see cref="System.Data.Common.DbDataReader"/></returns>
        public IDataReader GetDataReader(string sqlQuery, object param = null)
        {
            return this.storageProvider.GetDataReader(sqlQuery, param);
        }

        /// <summary>
        /// 获取 DataReader 对象
        /// 在关闭 DataReader 时，也会同时关闭对数据库的连接
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句
        /// 注：用具体的列名，不要使用 * 匹配符</param>
        /// <param name="start">分页起始数</param>
        /// <param name="length">每页的数量</param>
        /// <param name="param">条件子句参数
        /// 如：SELECT Name, Age, Enabled FROM Person WHERE Name = @Name AND Enabled = @Enabled
        /// 参数 new { Name = "gang.yang", Enabled = true }
        /// </param>
        /// <returns><see cref="System.Data.Common.DbDataReader"/></returns>
        public IDataReader GetDataReader(string sqlQuery, int start, int length, object param = null)
        {
            return this.storageProvider.GetDataReader(sqlQuery, start, length, param);
        }

        #endregion
    }
}
