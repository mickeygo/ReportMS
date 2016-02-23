using System.Collections.Generic;
using System.Text;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Reports.Dao;
using ReportMS.ServiceContracts;

namespace ReportMS.Reports.Services
{
    /// <summary>
    /// 报表数据模式查询服务
    /// </summary>
    public class ReportSchemaQueryService : IReportSchemaQueryService
    {
        #region IReportSchemaQueryService Members

        public IEnumerable<DatabaseSchemaDto> GetDatabaseSchema(string connectionName, string database)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine("WITH T AS (");
            sqlQuery.AppendLine("   SELECT	TABLE_CATALOG AS TableCatalog, TABLE_SCHEMA AS TableSchema, TABLE_NAME AS TableName, TABLE_TYPE AS TableType ");
            sqlQuery.AppendLine("   FROM    INFORMATION_SCHEMA.TABLES ");
            sqlQuery.AppendLine("   UNION ALL ");
            sqlQuery.AppendLine("   SELECT	TABLE_CATALOG AS TableCatalog, TABLE_SCHEMA AS TableSchema, TABLE_NAME AS TableName, 'VIEW' AS TableType ");
            sqlQuery.AppendLine("   FROM    INFORMATION_SCHEMA.VIEWS ");
            sqlQuery.AppendLine(") ");
            sqlQuery.AppendLine(" SELECT    TableCatalog, TableSchema, TableName, TableType ");
            sqlQuery.AppendLine(" ROM       T ");
            sqlQuery.AppendLine(" WHERE     TableCatalog = @TableCatalog ");

            return DatabaseReader.Create(connectionName).Reader.Select<DatabaseSchemaDto>(sqlQuery.ToString(), new { TableCatalog = database });
        }

        public IEnumerable<DatabaseSchemaDto> GetDatabaseSchema(string connectionName)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine("WITH T AS (");
            sqlQuery.AppendLine("   SELECT	TABLE_CATALOG AS TableCatalog, TABLE_SCHEMA AS TableSchema, TABLE_NAME AS TableName, TABLE_TYPE AS TableType ");
            sqlQuery.AppendLine("   FROM    INFORMATION_SCHEMA.TABLES ");
            sqlQuery.AppendLine("   UNION ALL ");
            sqlQuery.AppendLine("   SELECT	TABLE_CATALOG AS TableCatalog, TABLE_SCHEMA AS TableSchema, TABLE_NAME AS TableName, 'VIEW' AS TableType ");
            sqlQuery.AppendLine("   FROM    INFORMATION_SCHEMA.VIEWS ");
            sqlQuery.AppendLine(") ");
            sqlQuery.AppendLine(" SELECT    TableCatalog, TableSchema, TableName, TableType ");
            sqlQuery.AppendLine(" ROM       T ");

            return DatabaseReader.Create(connectionName).Reader.Select<DatabaseSchemaDto>(sqlQuery.ToString());
        }

        public IEnumerable<TableSchemaDto> GetTableSchema(string connectionName, string database, string schema, string table)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine(" SELECT	TABLE_CATALOG AS TableCatalog, TABLE_SCHEMA AS TableSchema, TABLE_NAME AS TableName ");
            sqlQuery.AppendLine("           ,COLUMN_NAME AS ColunmName, DATA_TYPE AS OriginalDataType, ORDINAL_POSITION AS OrdinalPosition, CHARACTER_MAXIMUM_LENGTH AS MaximumLength ");
            sqlQuery.AppendLine("           ,CASE   DATA_TYPE");
            sqlQuery.AppendLine("                   WHEN 'int' THEN 'integer' ");
            sqlQuery.AppendLine("                   WHEN 'bit' THEN 'integer' ");
            sqlQuery.AppendLine("                   WHEN 'float' THEN 'number' ");
            sqlQuery.AppendLine("                   WHEN 'datetime' THEN 'date' ");
            sqlQuery.AppendLine("                   ELSE 'string' ");
            sqlQuery.AppendLine("           END AS DataType ");
            sqlQuery.AppendLine(" FROM	    INFORMATION_SCHEMA.COLUMNS ");
            sqlQuery.AppendLine(" WHERE	    TABLE_CATALOG = @TableCatalog AND TABLE_SCHEMA = @TableSchema AND TABLE_NAME = @TableName ");

            return DatabaseReader.Create(connectionName).Reader.Select<TableSchemaDto>(sqlQuery.ToString(), 
                new { TableCatalog = database, TableSchema = schema, TableName = table });
        }

        public IEnumerable<TableSchemaDto> GetTableSchema(string connectionName, string schema, string table)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine(" SELECT	TABLE_CATALOG AS TableCatalog, TABLE_SCHEMA AS TableSchema, TABLE_NAME AS TableName ");
            sqlQuery.AppendLine("           ,COLUMN_NAME AS ColunmName, DATA_TYPE AS OriginalDataType, ORDINAL_POSITION AS OrdinalPosition, CHARACTER_MAXIMUM_LENGTH AS MaximumLength ");
            sqlQuery.AppendLine("           ,CASE   DATA_TYPE");
            sqlQuery.AppendLine("                   WHEN 'int' THEN 'integer' ");
            sqlQuery.AppendLine("                   WHEN 'bit' THEN 'integer' ");
            sqlQuery.AppendLine("                   WHEN 'float' THEN 'number' ");
            sqlQuery.AppendLine("                   WHEN 'datetime' THEN 'date' ");
            sqlQuery.AppendLine("                   ELSE 'string' ");
            sqlQuery.AppendLine("           END AS DataType ");
            sqlQuery.AppendLine(" FROM	    INFORMATION_SCHEMA.COLUMNS ");
            sqlQuery.AppendLine(" WHERE	    TABLE_SCHEMA = @TableSchema AND TABLE_NAME = @TableName ");

            return DatabaseReader.Create(connectionName).Reader.Select<TableSchemaDto>(sqlQuery.ToString(),
                new { TableSchema = schema, TableName = table });
        }

        #endregion
    }
}
