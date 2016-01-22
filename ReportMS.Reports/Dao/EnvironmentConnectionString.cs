using System.Diagnostics;

namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    public class EnvironmentConnectionString
    {
        private readonly string debug = "Debug";
        private string connectionString;

        private EnvironmentConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string GetDbConnectionName()
        {
            this.GetConnectionStringDebug();
            return this.connectionString;
        }

        /// <summary>
        /// 获取DB连接名
        /// </summary>
        /// <param name="connectionStringName">DB 连接名</param>
        /// <returns></returns>
        public static string GetDbConnectionName(string connectionStringName)
        {
            var connection = new EnvironmentConnectionString(connectionStringName);
            return connection.GetDbConnectionName();
        }

        [Conditional("DEBUG")]
        void GetConnectionStringDebug()
        {
            this.connectionString = string.Format("{0}{1}", this.connectionString, this.debug);
        }
    }
}
