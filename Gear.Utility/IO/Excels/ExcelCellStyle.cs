using System;
using NPOI.SS.UserModel;

namespace Gear.Utility.IO.Excels
{
    /// <summary>
    /// Excel 单元格样式
    /// </summary>
    internal class ExcelCellStyle
    {
        private readonly IWorkbook _workbook;

        /// <summary>
        /// 初始化<c>ExcelCellStyle</c>实例
        /// </summary>
        /// <param name="workbook"><c>IWorkbook</c>Excel 工作薄对象</param>
        public ExcelCellStyle(IWorkbook workbook)
        {
            this._workbook = workbook;
        }

        /// <summary>
        /// 建置单元格样式
        /// </summary>
        /// <param name="cellStyle">Excel 单元格样式</param>
        /// <returns></returns>
        public ICellStyle BuildCellStyle(CellStyle cellStyle)
        {
            var style = this._workbook.CreateCellStyle();
            var dataFormat = this._workbook.CreateDataFormat();
            switch (cellStyle)
            {
                case CellStyle.Date:
                    style.DataFormat = dataFormat.GetFormat("yyyy/MM/dd");
                    break;
                case CellStyle.DateTime:
                    style.DataFormat = dataFormat.GetFormat("yyyy/MM/dd HH:mm:ss"); // 24 时制
                    break;
                case CellStyle.Number2:
                    style.DataFormat =dataFormat.GetFormat("0.00");
                    break;
                case CellStyle.Number3:
                    style.DataFormat = dataFormat.GetFormat("0.000");
                    break;
                case CellStyle.Percent:
                    style.DataFormat = dataFormat.GetFormat("0.00%");
                    break;
                case CellStyle.Money:
                    style.DataFormat = dataFormat.GetFormat("￥#,##0");
                    break;
            }

            return style;
        }
    }

    /// <summary>
    /// Excel 单元格样式
    /// </summary>
    [Flags]
    public enum CellStyle
    {
        /// <summary>
        /// 单元格 头
        /// </summary>
        Header,

        /// <summary>
        /// 单元格为日期类型
        /// </summary>
        Date,

        /// <summary>
        /// 单元格为日期时间类型
        /// </summary>
        DateTime,

        /// <summary>
        /// 单元格为货币类型
        /// </summary>
        Money,

        /// <summary>
        /// 单元格为百分比类型
        /// </summary>
        Percent,

        /// <summary>
        /// 单元格为数字，保留两位小数
        /// </summary>
        Number2,

        /// <summary>
        /// 单元格为数字，保留三位小数
        /// </summary>
        Number3
    }
}
