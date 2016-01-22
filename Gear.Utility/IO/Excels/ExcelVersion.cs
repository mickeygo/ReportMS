using System;

namespace Gear.Utility.IO.Excels
{
    /// <summary>
    /// Excel 版本
    /// </summary>
    [Flags]
    public enum ExcelVersion
    {
        /// <summary>
        /// Excel 2003 版本
        /// </summary>
        Excel2003 = 1,

        /// <summary>
        /// Excel 2007、2010版本
        /// </summary>
        Excel2007 = 2
    }
}
