using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportMS.Reports.DataTables
{
    /// <summary>
    /// DataTables 参数
    /// </summary>
    public static class DataTablesOption
    {
        /// <summary>
        /// 获取页的索引, 默认为 0.
        /// DataTables 中页索引值从 0 开始，根据实际情况, 可选择将页索引值加一位
        /// </summary>
        public static int Start
        {
            get { return GetIntValueFromWebForm("start"); }
        }

        /// <summary>
        /// 获取每页的数据数目
        /// </summary>
        public static int Length
        {
            get { return GetIntValueFromWebForm("length"); }
        }

        /// <summary>
        /// Draw
        /// </summary>
        public static int Draw
        {
            get { return GetIntValueFromWebForm("draw"); }
        }

        static int GetIntValueFromWebForm(string name, int defaultValue = 0)
        {
            var namevalueforms = HttpContext.Current.Request.Form;
            var obj = namevalueforms[name];
            int result;
            return Int32.TryParse(obj, out result) ? result : defaultValue;
        }
    }

    /// <summary>
    /// UI DataTables 
    /// </summary>
    /// <typeparam name="T">要查询的对象类型</typeparam>
    public class DataTables<T>
    {
        /// <summary>
        /// 当前页面的数据
        /// </summary>
        /// <param name="currentPageItems"></param>
        /// <param name="totalItem">总共的数据</param>
        public DataTables(IEnumerable<T> currentPageItems, int totalItem)
        {
            this.RecordsTotal = this.RecordsFiltered = totalItem;
            this.Data = currentPageItems;
        }

        /// <summary>
        /// 初始化<c>DataTables</c>对象
        /// </summary>
        /// <param name="items">实体集</param>
        public DataTables(IEnumerable<T> items)
        {
            this.SetTableData(items);
        }

        /// <summary>
        /// 设置 Table 数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="items">对象集合</param>
        private void SetTableData(IEnumerable<T> items)
        {
            this.RecordsTotal = this.RecordsFiltered = items.Count();
            this.Data = items.Skip(DataTablesOption.Start).Take(DataTablesOption.Length).ToList();
        }

        /// <summary>
        /// 包装 DataTables 对象
        /// </summary>
        /// <returns></returns>
        public object WrapDataTablesObject()
        {
            if (this.Data == null)
                throw new InvalidOperationException("the data has not set.");

            return new { draw = DataTablesOption.Draw, recordsTotal = this.RecordsTotal, recordsFiltered = this.RecordsFiltered, data = this.Data };
        }

        #region Public Properties

        /// <summary>
        /// 获取数据总数目
        /// </summary>
        public int RecordsTotal { get; private set; }

        /// <summary>
        /// 获取筛选数据总数目
        /// </summary>
        public int RecordsFiltered { get; private set; }

        /// <summary>
        /// 获取数据
        /// </summary>
        public object Data { get; private set; }

        #endregion
    }
}
