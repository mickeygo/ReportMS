using System.Web.Hosting;

namespace ReportMS.Web.Client.IO.Files
{
    /// <summary>
    /// 虚拟路径
    /// </summary>
    public class VirtualDirectory
    {
        #region Fields

        /// <summary>
        /// 附件目录
        /// </summary>
        public const string AttachmentDir = "Attachment";

        /// <summary>
        /// 附件上传目录
        /// </summary>
        public const string AttachmentUploadDir = "AttachmentUpload";

        /// <summary>
        /// 附件下载目录
        /// </summary>
        public const string AttachmentDownloadDir = "AttachmentDownload";

        /// <summary>
        /// 附件临时目录
        /// </summary>
        public const string AttachmentTempDir = "AttachmentTemp";

        #endregion

        #region Properties

        /// <summary>
        /// 获取保存附件的物理目录路径
        /// </summary>
        public string AttachmentPhysicalPath
        {
            get { return GetPhysicalPathPrefix() + AttachmentDir; }
        }

        /// <summary>
        /// 获取附件上传的物理目录路径
        /// </summary>
        public string AttachmentUploadPhysicalPath
        {
            get { return GetPhysicalPathPrefix() + AttachmentUploadDir; }
        }

        /// <summary>
        /// 获取附件下载的的物理目录路径
        /// </summary>
        public string AttachmentDownloadPhysicalPath
        {
            get { return GetPhysicalPathPrefix() + AttachmentDownloadDir; }
        }

        /// <summary>
        /// 获取附件临时保存的物理目录路径
        /// </summary>
        public string AttachmentTempPhysicalPath
        {
            get { return GetPhysicalPathPrefix() + AttachmentTempDir; }
        }

        #endregion

        #region Private Methods

        private string GetPhysicalPathPrefix()
        {
            return HostingEnvironment.MapPath("~/");
        }

        #endregion
    }
}
