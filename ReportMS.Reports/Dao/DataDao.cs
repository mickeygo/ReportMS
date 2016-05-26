using System.Collections.Generic;
using System.Data;
using Gear.Infrastructure.Storage.Config;

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

        public IEnumerable<T> Query<T>(string connectionName, string sqlQuery, IDictionary<string, object> parameters,
            int start, int length)
        {
            return DatabaseReader.Create(connectionName).Reader.Select<T>(sqlQuery, start, length, parameters);
        }

        public IDataReader GetDataReader(string connectionName, string sqlQuery, IDictionary<string, object> parameters)
        {
            return DatabaseReader.Create(connectionName).Reader.GetDataReader(sqlQuery, parameters);
        }

        public IEnumerable<T> Query<T>(ConnectionOptions connectionOptions, string providerName, string sqlQuery,
            IDictionary<string, object> parameters)
        {
            return DatabaseReader.Create(connectionOptions, providerName).Reader.Select<T>(sqlQuery, parameters);
        }

        public int QueryCount(ConnectionOptions connectionOptions, string providerName, string sqlQuery,
            IDictionary<string, object> parameters)
        {
            return DatabaseReader.Create(connectionOptions, providerName).Reader.GetRecordCount(sqlQuery, parameters);
        }

        public IEnumerable<T> Query<T>(ConnectionOptions connectionOptions, string providerName, string sqlQuery,
            IDictionary<string, object> parameters, int start, int length)
        {
            return DatabaseReader.Create(connectionOptions, providerName)
                                 .Reader.Select<T>(sqlQuery, start, length, parameters);
        }

        public IDataReader GetDataReader(ConnectionOptions connectionOptions, string providerName, string sqlQuery,
            IDictionary<string, object> parameters)
        {
            return DatabaseReader.Create(connectionOptions, providerName).Reader.GetDataReader(sqlQuery, parameters);
        }

        #endregion
    }
}
