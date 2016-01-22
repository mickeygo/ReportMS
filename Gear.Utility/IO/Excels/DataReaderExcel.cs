using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gear.Utility.IO.Excels
{
    /// <summary>
    /// 由 DataBase 中获取的 DataReader 生成的Excel
    /// </summary>
    internal class DataReaderExcel : ExcelRecord, IExcel
    {
        private readonly IDataReader _dataReader;
        private readonly string _sheetName;

        /// <summary>
        /// 初始化<c>DataReaderExcel</c>对象
        /// </summary>
        /// <param name="sheetName">Sheet 名称</param>
        /// <param name="dataReader">要生成Excel的只读数据流</param>
        public DataReaderExcel(string sheetName, IDataReader dataReader)
        {
            this._sheetName = sheetName;
            this._dataReader = dataReader;
        }

        private IDictionary<string, Type> GetDataReaderColumnsInfo()
        {
            var readerSchema = this._dataReader.GetSchemaTable();
            if (readerSchema == null)
                return null;

            var dataTypeIndex = 12;
            return (from row in readerSchema.Rows.Cast<DataRow>()
                    select new { ColumnName = row[0].ToString(), DataType = (Type)row[dataTypeIndex] }).ToDictionary(s => s.ColumnName, s => s.DataType);
        }

        protected override string SetSheetName()
        {
            return this._sheetName;
        }

        protected override IDictionary<string, Type> SetSheetColumns()
        {
            return this.GetDataReaderColumnsInfo();
        }

        protected override IEnumerable<object[]> SetSheetColumnDatas()
        {
            using (this._dataReader)
            {
                while (this._dataReader.Read())
                {
                    var objs = new object[this._dataReader.FieldCount];
                    this._dataReader.GetValues(objs);
                    yield return objs;
                }
            }
        }
    }
}
