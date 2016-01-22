using System.Diagnostics;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 根据不同的开发环境，连接到对应的Db 连接字符串名
    /// </summary>
    internal class EnvironmentConnection
    {
        private readonly string debug = "Debug";
        private string connectionString;

        private EnvironmentConnection(string connectionString)
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
        /// <returns>Debug 模式下，字符串连接名后会加上 "Debug" 字符</returns>
        public static string GetDbConnectionName(string connectionStringName)
        {
            var connection = new EnvironmentConnection(connectionStringName);
            return connection.GetDbConnectionName();
        }

        [Conditional("DEBUG")]
        void GetConnectionStringDebug()
        {
            this.connectionString = string.Format("{0}{1}", this.connectionString, this.debug);
        }
    }
}
