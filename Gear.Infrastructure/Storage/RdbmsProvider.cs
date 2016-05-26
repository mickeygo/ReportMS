using System;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 数据库程序提供者
    /// </summary>
    public static class RdbmsProvider
    {
        /// <summary>
        /// 数据源程序提供者集合
        /// </summary>
        public static readonly string[] Providers = { MSSQL, Oracle, MySql, SQLite };

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

        /// <summary>
        /// 获取关系型数据库系统提供者
        /// </summary>
        /// <param name="rdbms"><see cref="RDBMS"/>关系型数据库提供者</param>
        /// <returns>关系型数据库提供者驱动</returns>
        public static string GetRdbmsProvider(RDBMS rdbms)
        {
            switch (rdbms)
            {
                case RDBMS.MSSQL:
                    return MSSQL;
                case RDBMS.Oracle:
                    return Oracle;
                case RDBMS.MySql:
                    return MySql;
                case RDBMS.SQLite:
                    return SQLite;
                default:
                    throw new InvalidOperationException("No found the RDBMS.");
            }
        }
    }

    /// <summary>
    /// 关系型数据库
    /// </summary>
    public enum RDBMS
    {
        /// <summary>
        /// SQL Server 数据库
        /// </summary>
        MSSQL = 1,

        /// <summary>
        /// Oracle 数据库
        /// </summary>
        Oracle = 2,

        /// <summary>
        /// MySql 数据库
        /// </summary>
        MySql = 3,

        /// <summary>
        /// SQLite 数据库
        /// </summary>
        SQLite = 4
    }
}
