using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// SQL 参数生成器
    /// </summary>
    public class SqlParameterBuilder
    {
        private readonly Dictionary<string, SqlParameter> parameters = new Dictionary<string, SqlParameter>();

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">值</param>
        public void AddParameterValue(string name, string value)
        {
            var sqlParam = new SqlParameter(name, value);
            this.parameters.Add(name, sqlParam);
        }

        /// <summary>
        /// 获取参数集
        /// </summary>
        /// <returns>DbParameter 集合</returns>
        public DbParameter[] GetParameters()
        {
            return this.parameters.Select(p => p.Value).OfType<DbParameter>().ToArray();
        }
    }
}
