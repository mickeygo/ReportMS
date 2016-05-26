using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure.Services.ApplicationServices;
using Gear.Infrastructure.Storage.Config;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为报表数据模式查询服务
    /// </summary>
    [ServiceContract]
    public interface IReportSchemaQueryService : IApplicationQueryService
    {
        /// <summary>
        /// 获取数据库模式
        /// </summary>
        /// <param name="connectionOptions">数据源连接选项</param>
        /// <param name="providerName">数据源连接提供者</param>
        /// <param name="database">数据库名称</param>
        /// <returns>数据库模式 Dto 集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<DatabaseSchemaDto> GetDatabaseSchema(ConnectionOptions connectionOptions, string providerName, string database);

        /// <summary>
        /// 获取数据库模式
        /// </summary>
        /// <param name="connectionOptions">数据源连接选项</param>
        /// <param name="providerName">数据源连接提供者</param>
        /// <returns>数据库模式 Dto 集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<DatabaseSchemaDto> GetDatabaseSchema(ConnectionOptions connectionOptions, string providerName);

        /// <summary>
        /// 获取数据表模式
        /// </summary>
        /// <param name="connectionOptions">数据源连接选项</param>
        /// <param name="providerName">数据源连接提供者</param>
        /// <param name="database">数据库</param>
        /// <param name="schema">数据库模式</param>
        /// <param name="table">数据表名</param>
        /// <returns>数据库中 Table 模式 Dto 集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<TableSchemaDto> GetTableSchema(ConnectionOptions connectionOptions, string providerName, string database, string schema, string table);

        /// <summary>
        /// 获取数据表模式
        /// </summary>
        /// <param name="connectionOptions">数据源连接选项</param>
        /// <param name="providerName">数据源连接提供者</param>
        /// <param name="schema">数据库模式</param>
        /// <param name="table">数据表名</param>
        /// <returns>数据库中 Table 模式 Dto 集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<TableSchemaDto> GetTableSchema(ConnectionOptions connectionOptions, string providerName, string schema, string table);
    }
}
