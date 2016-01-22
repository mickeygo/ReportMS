using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Gear.Infrastructure.Web.Outputs
{
    /// <summary>
    /// 基于 MVC 的 FileResult 的文件输出。
    /// Http Response OutPutStream 流中文件输出
    /// </summary>
    public class MvcFileOutput : IOutput
    {
        #region Private Fields

        private const string ExcelExt = ".xlsx";
        private readonly ControllerContext context;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个<c>FileOutput</c>实例
        /// </summary>
        /// <param name="context">Controller 上下文</param>
        public MvcFileOutput(ControllerContext context)
        {
            this.context = context;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 导出 Excel（.xlsx 格式）
        /// </summary>
        /// <param name="fileContents">以字节的形式呈现的文件内容</param>
        /// <param name="outputName">要输出的 Excel 名称，不含扩展名</param>
        public void OutPutExcel(byte[] fileContents, string outputName)
        {
            var fileName = SetFileNameUseSpecifiedExtension(outputName, ExcelExt);
            this.OutPut(fileContents, fileName);
        }

        /// <summary>
        /// 导出 Excel（.xlsx 格式）
        /// </summary>
        /// <param name="fileStream">以流的形式呈现的文件内容</param>
        /// <param name="outputName">要输出的 Excel 名称，不含扩展名</param>
        public void OutPutExcel(Stream fileStream, string outputName)
        {
            var fileName = SetFileNameUseSpecifiedExtension(outputName, ExcelExt);
            this.OutPut(fileStream, fileName);
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="filePath">要导出的文件的绝对路径</param>
        /// <param name="outputName">要输出的文件名称, 含扩展名; 若没有输入，则为指定的文件的文件名</param>
        public void OutPut(string filePath, string outputName = null)
        {
            var fileName = outputName ?? Path.GetFileName(filePath);
            var fileResult = new FilePathResult(filePath, MimeMapping.GetMimeMapping(fileName))
            {
                FileDownloadName = fileName
            };

            fileResult.ExecuteResult(this.context);
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="fileContents">字节数组</param>
        /// <param name="outputName">要输出的文件名称，含扩展名</param>
        public void OutPut(byte[] fileContents, string outputName)
        {
            var fileResult = new FileContentResult(fileContents, MimeMapping.GetMimeMapping(outputName))
            {
                FileDownloadName = outputName
            };

            fileResult.ExecuteResult(this.context);
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="fileStream">文件流。流数据最终会在文件导出后(即使出错)关闭并释放</param>
        /// <param name="outputName">要输出的文件名称，含扩展名</param>
        public void OutPut(Stream fileStream, string outputName)
        {
            var fileResult = new FileStreamResult(fileStream, MimeMapping.GetMimeMapping(outputName))
            {
                FileDownloadName = outputName
            };

            fileResult.ExecuteResult(this.context);
        }

        #endregion

        #region Private Methods

        private string SetFileNameUseSpecifiedExtension(string fileName, string extension)
        {
            return Path.ChangeExtension(fileName, extension);
        }

        #endregion
    }
}
