using System;

namespace Gear.Infrastructure.Storage.Config
{
    /// <summary>
    /// 数据源连接选项
    /// </summary>
    public class ConnectionOptions
    {
        /// <summary>
        /// 连接的数据源服务器
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 连接的数据源
        /// </summary>
        public string InitialCatalog { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据库是否只读
        /// </summary>
        public bool? ReadOnly { get; set; }

        // Override, 输出连接字符串
        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(this.DataSource)
                || String.IsNullOrWhiteSpace(this.InitialCatalog)
                || String.IsNullOrWhiteSpace(this.UserId)
                || String.IsNullOrWhiteSpace(this.Password))
            {
                throw new InvalidOperationException("The connection string argument is missing.");
            }

            var connectionString = string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3}",
                this.DataSource, this.InitialCatalog, this.UserId, this.Password);

            if (this.ReadOnly ?? false)
                connectionString += ";ApplicationIntent=ReadOnly";

            return connectionString;
        }
    }
}
