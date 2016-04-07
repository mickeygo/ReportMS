namespace ReportMS.Domain.Models.ReportModule.DatabaseAggregate
{
    /// <summary>
    /// 数据库程序提供者
    /// </summary>
    public class DatabaseProvider
    {
        /// <summary>
        /// SQL Server 数据库程序提供者
        /// </summary>
        public const string MSSQL = "System.Data.SqlClient";

        /// <summary>
        /// Oracle 数据库程序提供者
        /// </summary>
        public const string Oracle = "Oracle.ManagedDataAccess.Client";

        /// <summary>
        /// MySql 数据库程序提供者
        /// </summary>
        public const string MySql = "MySql.Data.MySqlClient";

        /// <summary>
        /// SQLite 数据库程序提供者
        /// </summary>
        public const string SQLite = "System.Data.SQLite";
    }
}
