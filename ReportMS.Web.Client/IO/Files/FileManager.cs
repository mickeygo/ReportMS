using System.IO;

namespace ReportMS.Web.Client.IO.Files
{
    /// <summary>
    /// 文件管理
    /// </summary>
    public class FileManager
    {
        // Todo: Complete the file manage function.

        /// <summary>
        /// 获得虚拟目录
        /// </summary>
        public VirtualDirectory VirtualDirectory
        {
            get { return new VirtualDirectory(); }
        }

        #region Private Methods

        private void EnsureExistDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        #endregion
    }
}
