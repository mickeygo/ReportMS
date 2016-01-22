using System.Collections.Generic;
using ReportMS.Reports.ReadModel;

namespace ReportMS.Reports.Managers
{
    /// <summary>
    /// 表示呈现类是只读报表
    /// </summary>
    public interface IReportRead
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns><c>ReportData</c>集合</returns>
        IEnumerable<ReportData> ExecuteSqlQuery();

        /// <summary>
        /// 执行 DataTables 数据查询
        /// </summary>
        /// <returns>返回 DataTables 包装好的对象</returns>
        object ExecuteDataTablesQuery();

        /// <summary>
        /// 导出 Excel，返回 Excel 的字节集合
        /// </summary>
        /// <param name="sheetName">Excel Sheet 名称</param>
        /// <returns><c>byte</c>集合</returns>
        byte[] ExecuteExcelExport(string sheetName);

        /// <summary>
        /// 获取 Table 或 View 名
        /// </summary>
        string TableOrViewName { get; }
    }
}
