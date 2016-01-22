using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gear.Utility.IO.Excels
{
    /// <summary>
    /// 有集合数据生成 Excel
    /// </summary>
    /// <typeparam name="T">要生成 Excel 的数据对象类型</typeparam>
    internal class CollectionExcel<T> : ExcelRecord, IExcel
        where T : class, new()
    {
        private readonly IEnumerable<T> _data;
        private readonly string _sheetName;

        /// <summary>
        /// 初始化<c>CollectionExcel</c>实例
        /// </summary>
        /// <param name="sheetName">Sheet 名称</param>
        /// <param name="datas">数据集合</param>
        public CollectionExcel(string sheetName, IEnumerable<T> datas)
        {
            this._sheetName = sheetName;
            this._data = datas;
        }

        protected override string SetSheetName()
        {
            return this._sheetName;
        }

        protected override IDictionary<string, Type> SetSheetColumns()
        {
            return (from property in TypeDescriptor.GetProperties(typeof (T)).Cast<PropertyDescriptor>()
                    select new {property.Name, property.PropertyType}).ToDictionary(s => s.Name, s => s.PropertyType);
        }

        protected override IEnumerable<object[]> SetSheetColumnDatas()
        {
            return this._data.Select(data => 
                (from property in TypeDescriptor.GetProperties(typeof (T)).Cast<PropertyDescriptor>()
                select property.GetValue(data)).ToArray());
        }
    }
}
