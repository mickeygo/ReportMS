using System.Collections.Generic;
using System.Data;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 表示实现接口类为储存器提供者
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// 建制数据源连接
        /// </summary>
        /// <param name="connectionString">数据源连接字符串</param>
        void BuildConnection(string connectionString);

        /// <summary>
        /// 获取第一个对象，若没有，则返回为 null
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="param">查询参数</param>
        /// <returns><c>T</c></returns>
        T SelectFirstOrDefault<T>(string sqlQuery, object param = null);

        /// <summary>
        /// 获取在存储容器中的对象集合, 若没有数据，则返回空的集合（非 null）
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="param">查询参数</param>
        /// <returns><c>T</c>集合</returns>
        IEnumerable<T> Select<T>(string sqlQuery, object param = null);

        /// <summary>
        /// 获取在存储容器中的对象集合, 若没有数据，则返回空的集合（非 null）
        /// </summary>
        /// <typeparam name="T">要查询的实体类型</typeparam>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="start">分页起始数</param>
        /// <param name="length">每页的数量</param>
        /// <param name="param">查询参数</param>
        /// <returns><c>T</c>集合</returns>
        IEnumerable<T> Select<T>(string sqlQuery, int start, int length, object param = null);

        /// <summary>
        /// 获取存储容器中的对象数量
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="param">查询参数</param>
        /// <returns><see cref="System.Int32"/></returns>
        int GetRecordCount(string sqlQuery, object param = null);

        /// <summary>
        /// 获取 DataReader 对象
        /// 在关闭 DataReader 时，也会同时关闭对数据库的连接
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="param">查询参数</param>
        /// <returns><see cref="System.Data.Common.DbDataReader"/></returns>
        IDataReader GetDataReader(string sqlQuery, object param = null);

        /// <summary>
        /// 获取 DataReader 对象
        /// 在关闭 DataReader 时，也会同时关闭对数据库的连接
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="start">分页起始数</param>
        /// <param name="length">每页的数量</param>
        /// <param name="param">查询参数</param>
        /// <returns><see cref="System.Data.Common.DbDataReader"/></returns>
        IDataReader GetDataReader(string sqlQuery, int start, int length, object param = null);
    }
}
