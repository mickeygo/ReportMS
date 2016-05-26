using System.Collections.Generic;
using System.Data;
using Gear.Infrastructure.Storage.Config;

namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// 表示实现类是报表访问数据
    /// </summary>
    public interface IDataDao
    {
        /// <summary>
        /// 查询 ReportData 集合
        /// </summary>
        /// <typeparam name="T">要查询的类型</typeparam>
        /// <param name="connectionName">数据源连接名</param>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>ReportData 集合</returns>
        IEnumerable<T> Query<T>(string connectionName, string sqlQuery, IDictionary<string, object> parameters);

        /// <summary>
        /// 查询对象数量
        /// </summary>
        /// <param name="connectionName">数据源连接名</param>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果数目</returns>
        int QueryCount(string connectionName, string sqlQuery, IDictionary<string, object> parameters);

        /// <summary>
        /// 查询 ReportData 集合
        /// </summary>
        /// <typeparam name="T">要查询的对象类型</typeparam>
        /// <param name="connectionName">数据源连接名</param>
        /// <param name="start">起始数, 表示从第多少行数据开始取值（包括此行数据）</param>
        /// <param name="length">取多少数据，从起始数开始，如从 10 行开始，取 6 长度，结果集为 [10, 15]</param>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns><c>ReportData</c>集合</returns>
        IEnumerable<T> Query<T>(string connectionName, string sqlQuery, IDictionary<string, object> parameters,
            int start, int length);

        /// <summary>
        /// 获取 DataReader 对象
        /// </summary>
        /// <param name="connectionName">数据源连接名</param>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataReader 对象</returns>
        IDataReader GetDataReader(string connectionName, string sqlQuery, IDictionary<string, object> parameters);

        /// <summary>
        /// 查询 ReportData 集合
        /// </summary>
        /// <typeparam name="T">要查询的类型</typeparam>
        /// <param name="connectionOptions">数据源连接选项</param>
        /// <param name="providerName">数据源提供者</param>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>ReportData 集合</returns>
        IEnumerable<T> Query<T>(ConnectionOptions connectionOptions, string providerName, string sqlQuery,
            IDictionary<string, object> parameters);

        /// <summary>
        /// 查询对象数量
        /// </summary>
        /// <param name="connectionOptions">数据源连接选项</param>
        /// <param name="providerName">数据源提供者</param>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果数目</returns>
        int QueryCount(ConnectionOptions connectionOptions, string providerName, string sqlQuery,
            IDictionary<string, object> parameters);

        /// <summary>
        /// 查询 ReportData 集合
        /// </summary>
        /// <typeparam name="T">要查询的对象类型</typeparam>
        /// <param name="connectionOptions">数据源连接选项</param>
        /// <param name="providerName">数据源提供者</param>
        /// <param name="start">起始数, 表示从第多少行数据开始取值（包括此行数据）</param>
        /// <param name="length">取多少数据，从起始数开始，如从 10 行开始，取 6 长度，结果集为 [10, 15]</param>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns><c>ReportData</c>集合</returns>
        IEnumerable<T> Query<T>(ConnectionOptions connectionOptions, string providerName, string sqlQuery,
            IDictionary<string, object> parameters, int start, int length);

        /// <summary>
        /// 获取 DataReader 对象
        /// </summary>
        /// <param name="connectionOptions">数据源连接选项</param>
        /// <param name="providerName">数据源提供者</param>
        /// <param name="sqlQuery">SQL 查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataReader 对象</returns>
        IDataReader GetDataReader(ConnectionOptions connectionOptions, string providerName, string sqlQuery,
            IDictionary<string, object> parameters);
    }
}
