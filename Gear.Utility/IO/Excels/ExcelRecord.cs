using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Gear.Utility.IO.Excels
{
    /// <summary>
    /// Excel 生成记录抽象类
    /// </summary>
    internal abstract class ExcelRecord
    {
        private readonly IWorkbook _workbook;
        private string[] _columnNames;
        private Type[] _columnTypes;
        private IEnumerable<object[]> _datas;
        private readonly Dictionary<Enum, ICellStyle> _cellStyles = new Dictionary<Enum, ICellStyle>();

        /// <summary>
        /// 初始化<c>Excel</c>实例，Excel 为 2007 版本
        /// </summary>
        protected ExcelRecord()
            : this(ExcelVersion.Excel2007)
        { }

        /// <summary>
        /// 初始化<c>Excel</c>实例
        /// </summary>
        /// <param name="version">Excel 版本号</param>
        protected ExcelRecord(ExcelVersion version)
        {
            switch (version)
            {
                case ExcelVersion.Excel2003:
                    this._workbook = new HSSFWorkbook();
                    break;
                case ExcelVersion.Excel2007:
                    this._workbook = new XSSFWorkbook();
                    break;
                default:
                    throw new InvalidEnumArgumentException("The Excel version is not matched.");
            }
        }

        /// <summary>
        /// 创建 Sheet
        /// </summary>
        /// <returns>Sheet</returns>
        private ISheet CreateSheet()
        {
            var sheetName = this.SetSheetName();
            if (!this.CheckSheetName(sheetName))
                throw new InvalidOperationException("The sheet name is illegal");
            return this._workbook.CreateSheet(sheetName);
        }

        /// <summary>
        /// sheet 抬头
        /// </summary>
        /// <param name="sheet">sheet 对象</param>
        private void CreateRowHeader(ISheet sheet)
        {
            var row = sheet.CreateRow(0);
            for (var i = 0; i < this._columnNames.Length; i++)
            {
                var cell = row.CreateCell(i);
                this.WriteToCell(cell, typeof(string), this._columnNames[i]);
            }
            this.CreateRow(row, this._columnNames, false);
        }

        /// <summary>
        /// sheet 行主体
        /// </summary>
        /// <param name="sheet">sheet 对象</param>
        private void CreateRowBody(ISheet sheet)
        {
            if (this._datas == null)
                return;

            var i = 1;
            foreach (var data in this._datas)
            {
                var row = sheet.CreateRow(i++);
                this.CreateRow(row, data);
            }
        }

        /// <summary>
        /// 创建 Row
        /// </summary>
        /// <param name="row">Row 对象</param>
        /// <param name="datas">填入 Row 的只读数据集合</param>
        /// <param name="isFormat">是否数据有格式化，默认 true</param>
        private void CreateRow(IRow row, IReadOnlyList<object> datas, bool isFormat = true)
        {
            for (var i = 0; i < this._columnNames.Length; i++)
            {
                var cell = row.CreateCell(i);
                var dataType = isFormat ? this._columnTypes[i] : typeof (string);
                this.WriteToCell(cell, dataType, datas[i]);
            }
        }

        /// <summary>
        /// 写入值到单元格中
        /// </summary>
        /// <param name="cell">单元格对象</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据值</param>
        private void WriteToCell(ICell cell, Type dataType, object value)
        {
            if (dataType == typeof (Int16))
                cell.SetCellValue((Int16) value);
            else if (dataType == typeof (Int32))
                cell.SetCellValue((Int32) value);
            else if (dataType == typeof (Int64))
                cell.SetCellValue((Int64) value);
            else if (dataType == typeof (float))
                cell.SetCellValue((float) value);
            else if (dataType == typeof (double))
                cell.SetCellValue((double) value);
            else if (dataType == typeof (Boolean))
                cell.SetCellValue((bool) value);
            else if (dataType == typeof (DateTime))
            {
                cell.SetCellValue((DateTime) value);
                cell.CellStyle = this.GetCellStyle(CellStyle.Date);
            }
            else
                cell.SetCellValue((string) value);
        }

        /// <summary>
        /// 获取单元格样式
        /// </summary>
        /// <param name="cellStyle"></param>
        /// <returns><c>ICellStyle</c></returns>
        private ICellStyle GetCellStyle(CellStyle cellStyle)
        {
            if (this._cellStyles.ContainsKey(cellStyle))
                return this._cellStyles[cellStyle];

            var excelCellStyle = new ExcelCellStyle(this._workbook);
            var style = excelCellStyle.BuildCellStyle(cellStyle);
            this._cellStyles.Add(cellStyle, style);
            return style;
        }

        /// <summary>
        /// 检查文件名是否有非法字符
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>True, 表示没有非法字符；False 表示存在非法字符</returns>
        private bool CheckSheetName(string fileName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return fileName.All(s => !invalidChars.Contains(s));
        }

        /// <summary>
        /// 检查列名和数据容量是否能匹配
        /// </summary>
        private void CheckColumnsNameAndData()
        {
            if (!this._columnNames.Any())
                throw new Exception("The column names is null.");

            //if (this._datas != null && this._datas.Any())
            //{
            //    var isDataLess = this._datas.Any(s => s.Count() < this._columnNames.Length);
            //    if (isDataLess)
            //        throw new Exception("The column data is not matched the column name.");
            //}
        }

        /// <summary>
        /// 将数据 WorkBook 数据读到 Byte 数组中
        /// </summary>
        /// <returns></returns>
        private byte[] WriteToBytes()
        {
            using (var ms = new MemoryStream())
            {
                this._workbook.Write(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 设置 Sheet 名
        /// </summary>
        /// <returns></returns>
        protected abstract string SetSheetName();

        /// <summary>
        /// 设置 Sheet 列的字段名和字段类型
        /// </summary>
        /// <returns>字段名;字段类型</returns>
        protected abstract IDictionary<string, Type> SetSheetColumns();

        /// <summary>
        /// Sheet 数据集合
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object[]> SetSheetColumnDatas();

        private void Excute()
        {
            var sheetColumns = this.SetSheetColumns();
            this._columnNames = sheetColumns.Keys.ToArray();
            this._columnTypes = sheetColumns.Values.ToArray();
            this._datas = this.SetSheetColumnDatas();

            this.CheckColumnsNameAndData();

            var sheet = this.CreateSheet();
            this.CreateRowHeader(sheet);
            this.CreateRowBody(sheet);
        }

        /// <summary>
        /// 将 Excel 数据保存为 byte
        /// </summary>
        /// <returns><see cref="byte"/>字节集合</returns>
        public byte[] SaveAsBytes()
        {
            this.Excute();
            return this.WriteToBytes();
        }

        /// <summary>
        /// 将 Excel 数据写入 Stream
        /// </summary>
        /// <param name="stream">要被写入的流</param>
        public void SaveAsStream(Stream stream)
        {
            this.Excute();
            this._workbook.Write(stream);
        }
    }
}
