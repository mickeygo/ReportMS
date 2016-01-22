using System.Collections.Generic;
using System.Data;

namespace Gear.Utility.IO.Excels
{
    /// <summary>
    /// Excel 工厂
    /// </summary>
    public static class ExcelFactory
    {
        /// <summary>
        /// 创建 Excel 对象
        /// </summary>
        /// <typeparam name="T">要生成 Excel 的数据对象类型</typeparam>
        /// <param name="sheetName">Sheet 名称</param>
        /// <param name="datas">数据集合</param>
        /// <returns><c>IExcel</c></returns>
        public static IExcel Create<T>(string sheetName, IEnumerable<T> datas) 
            where T : class, new()
        {
            return new CollectionExcel<T>(sheetName, datas);
        }

        /// <summary>
        /// 创建 Excel 对象
        /// </summary>
        /// <param name="sheetName">Sheet 名称</param>
        /// <param name="dataReader">要生成 Excel 的只读数据流</param>
        /// <returns><c>IExcel</c></returns>
        public static IExcel Create(string sheetName, IDataReader dataReader)
        {
            return new DataReaderExcel(sheetName, dataReader);
        }
    }
}
