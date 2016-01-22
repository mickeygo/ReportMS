using System.Collections.Generic;
using System.Data;

namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// Report 访问数据
    /// </summary>
    public class DataDao : IDataDao
    {
        #region IReportDao Members

        public IEnumerable<T> Query<T>(string connectionName, string sqlQuery, IDictionary<string, object> parameters)
        {
            return DatabaseReader.Create(connectionName).Reader.Select<T>(sqlQuery, parameters);
        }

        public int QueryCount(string connectionName, string sqlQuery, IDictionary<string, object> parameters)
        {
            return DatabaseReader.Create(connectionName).Reader.GetRecordCount(sqlQuery, parameters);
        }

        public IEnumerable<T> Query<T>(string connectionName, string sqlQuery, IDictionary<string, object> parameters, int start, int lenth)
        {
            return DatabaseReader.Create(connectionName).Reader.Select<T>(sqlQuery, start, lenth, parameters);
        }

        public IDataReader GetDataReader(string connectionName, string sqlQuery, IDictionary<string, object> parameters)
        {
            return DatabaseReader.Create(connectionName).Reader.GetDataReader(sqlQuery, parameters);
        }

        #endregion
    }
}
