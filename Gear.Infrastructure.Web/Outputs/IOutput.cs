using System.IO;

namespace Gear.Infrastructure.Web.Outputs
{
    /// <summary>
    /// 表示实现的类为文件导出
    /// </summary>
    public interface IOutput
    {
        /// <summary>
        /// 导出 Excel（.xlsx 格式）
        /// </summary>
        /// <param name="fileContents">以字节的形式呈现的文件内容</param>
        /// <param name="outputName">要输出的 Excel 名称，不含扩展名</param>
        void OutPutExcel(byte[] fileContents, string outputName);

        /// <summary>
        /// 导出 Excel（.xlsx 格式）
        /// </summary>
        /// <param name="fileStream">以流的形式呈现的文件内容</param>
        /// <param name="outputName">要输出的 Excel 名称，不含扩展名</param>
        void OutPutExcel(Stream fileStream, string outputName);

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="filePath">要导出的文件的绝对路径</param>
        /// <param name="outputName">要输出的文件名称, 含扩展名; 若没有输入，则为指定的文件的文件名</param>
        void OutPut(string filePath, string outputName = null);

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="fileContents">字节数组</param>
        /// <param name="outputName">要输出的文件名称，含扩展名</param>
        void OutPut(byte[] fileContents, string outputName);

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="outputName">要输出的文件名称，含扩展名</param>
        void OutPut(Stream fileStream, string outputName);
    }
}
