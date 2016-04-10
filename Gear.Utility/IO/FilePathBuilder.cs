using System;
using System.Collections.Generic;
using System.IO;

namespace Gear.Utility.IO
{
    /// <summary>
    /// 文件路径构建者
    /// </summary>
    public class FilePathBuilder
    {
        private static readonly Dictionary<FileExtension, string> FileContainer = new Dictionary<FileExtension, string>
        {
            {FileExtension.Text, ".txt"},
            {FileExtension.Excel2003, ".xls"},
            {FileExtension.Excel2007, ".xlsx"},
            {FileExtension.CSV, ".csv"},
            {FileExtension.Word2003, ".doc"},
            {FileExtension.Word2007, ".docx"},
            {FileExtension.PDF, ".pdf"}
        };

        /// <summary>
        /// 构建文件路径。
        /// 若文件存在扩展名，则将扩展名换为指定的扩展名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns>文件路径</returns>
        public static string BuildPath(string fileName, FileExtension fileExtension)
        {
            var extension = FileContainer[fileExtension];

            if (Path.HasExtension(fileName))
            {
                var ext = Path.GetExtension(fileName);
                if (ext.Equals(extension, StringComparison.OrdinalIgnoreCase))
                    return fileName;

                return Path.ChangeExtension(fileName, FileContainer[fileExtension]);
            }

            return fileName + FileContainer[fileExtension];
        }
    }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public enum FileExtension
    {
        /// <summary>
        /// txt 文件
        /// </summary>
        Text,

        /// <summary>
        /// Excel 2003
        /// </summary>
        Excel2003,

        /// <summary>
        /// Excel 2007
        /// </summary>
        Excel2007,
        
        /// <summary>
        /// CSV
        /// </summary>
        CSV,
        
        /// <summary>
        /// Word 2003
        /// </summary>
        Word2003,
        
        /// <summary>
        /// Word 2007
        /// </summary>
        Word2007,

        /// <summary>
        /// PDF
        /// </summary>
        PDF
    }
}
