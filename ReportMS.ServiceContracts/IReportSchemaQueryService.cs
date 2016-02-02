using System.Collections.Generic;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为报表数据模式查询服务
    /// </summary>
    public interface IReportSchemaQueryService : IApplicationQueryService
    {
        /// <summary>
        /// 获取数据库模式
        /// </summary>
        /// <param name="database">数据库名称</param>
        /// <returns></returns>
        IEnumerable<DatabaseSchemaDto> GetDatabaseSchema(string database);

        /// <summary>
        /// 获取数据表模式
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="schema">数据库模式</param>
        /// <param name="table">数据表名</param>
        /// <returns></returns>
        IEnumerable<TableSchemaDto> GetTableSchema(string database, string schema, string table);
    }
}
