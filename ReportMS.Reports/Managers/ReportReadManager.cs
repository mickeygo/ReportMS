using System;
using System.Collections.Generic;
using Gear.Utility.IO.Excels;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Reports.Dao;
using ReportMS.Reports.ReadModel;
using ReportMS.Reports.DataTables;
using ReportMS.ServiceContracts;

namespace ReportMS.Reports.Managers
{
    /// <summary>
    /// 报表读取管理
    /// </summary>
    public class ReportReadManager : IReportRead
    {
        private readonly ReportSqlWrapper sqlWrapper = new ReportSqlWrapper();
        private readonly IDataDao dataDao;
        private readonly IReportQueryService _reportQueryServiceContract;
        private ReportDto report;

        #region Ctor

        /// <summary>
        /// 初始化<c>ReportReadManager</c>
        /// </summary>
        /// <param name="dataDao">报表数据访问对象</param>
        /// <param name="reportQueryServiceContract">报表查询服务</param>
        public ReportReadManager(IDataDao dataDao, IReportQueryService reportQueryServiceContract)
        {
            this.dataDao = dataDao;
            this._reportQueryServiceContract = reportQueryServiceContract;

            var tableId = this.sqlWrapper.GetReportTableOrViewId();
            if (tableId.HasValue)
                this.GetTableOrViewName(tableId.Value);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns><c>ReportData</c>集合</returns>
        public IEnumerable<ReportData> ExecuteSqlQuery()
        {
            var sqlQueryAndParms = this.GetSqlQueryAndParms(SelectClauseBuildMode.StringWithAlias);
            return this.dataDao.Query<ReportData>(this.report.Database, sqlQueryAndParms.Item1, sqlQueryAndParms.Item2);
        }

        /// <summary>
        /// 执行 DataTables 数据查询
        /// </summary>
        /// <returns>返回 DataTables 包装好的对象</returns>
        public object ExecuteDataTablesQuery()
        {
            var conn = this.report.Database;
            var sqlQueryAndParms = this.GetSqlQueryAndParms(SelectClauseBuildMode.StringWithAlias);

            var sqlQuery = sqlQueryAndParms.Item1;
            var sqlWhere = sqlQueryAndParms.Item2;
            var start = DataTablesOption.Start + 1; // 分页中 start 为起始行，不是要跳过的行值
            var length = DataTablesOption.Length;

            var itemCount = this.dataDao.QueryCount(conn, sqlQuery, sqlWhere);
            var items = this.dataDao.Query<ReportData>(conn, sqlQuery, sqlWhere, start, length);

            var datatable = new DataTables<ReportData>(items, itemCount);
            return datatable.WrapDataTablesObject();
        }

        /// <summary>
        /// 导出 Excel，返回 Excel 的字节集合
        /// </summary>
        /// <param name="sheetName">Excel Sheet 名称</param>
        /// <returns><c>byte</c>集合</returns>
        public byte[] ExecuteExcelExport(string sheetName)
        {
            var sqlQueryAndParms = this.GetSqlQueryAndParms(SelectClauseBuildMode.Raw);
            var reader = DatabaseReader.Create(this.report.Database)
                .Reader.GetDataReader(sqlQueryAndParms.Item1, sqlQueryAndParms.Item2);

            var excel = ExcelFactory.Create(sheetName, reader);
            return excel.SaveAsBytes();
        }

        /// <summary>
        /// 获取 Table 或 View 名
        /// </summary>
        public string TableOrViewName
        {
            get { return this.report.ReportName; }
        }

        #endregion

        #region Private Methods

        private void GetTableOrViewName(Guid tableOrViewId)
        {
            this.report = this._reportQueryServiceContract.GetReport(tableOrViewId, false);
            if (report == null)
                throw new Exception(String.Format("The table collection is not exists the table id [{0}].",
                    tableOrViewId.ToString()));
        }

        private Tuple<string, IDictionary<string, object>> GetSqlQueryAndParms(SelectClauseBuildMode mode)
        {
            this.sqlWrapper.ExecuteSqlBagBuilder(this.TableOrViewName, mode);

            var sqlQuery = this.sqlWrapper.GetSqlClause();
            var parameters = this.sqlWrapper.GetSqlParameters();

            return Tuple.Create(sqlQuery, parameters);
        }

        #endregion
    }
}
