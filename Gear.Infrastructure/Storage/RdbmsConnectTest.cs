using System;
using System.Data;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 关系型数据库连接测试
    /// </summary>
    public class RdbmsConnectTest
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// 初始化一个<c>RdbmsConnectTest</c>对象
        /// </summary>
        /// <param name="connection">数据库连接实例，且要已指定了连接字符串</param>
        public RdbmsConnectTest(IDbConnection connection)
        {
            this._connection = connection;
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns>true 表示测试成功；否则为 false</returns>
        public bool Test()
        {
            try
            {
                if (this._connection != null)
                    this._connection.Open();
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return false;
            }
            finally
            {
                if (this._connection != null && this._connection.State == ConnectionState.Open)
                    this._connection.Close();
            }

            return true;
        }

        /// <summary>
        /// 获取连接的错误消息
        /// </summary>
        public string ErrorMessage { get; private set; }
    }
}
