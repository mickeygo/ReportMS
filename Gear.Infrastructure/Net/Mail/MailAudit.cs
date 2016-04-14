using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gear.Infrastructure.Net.Mail
{
    /// <summary>
    /// 审计邮件信息
    /// </summary>
    public static class MailAudit
    {
        #region Properties

        /// <summary>
        /// Mail 收件人信息验证模型
        /// </summary>
        public const string StringValidator = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        #endregion

        #region Static Public Methods

        /// <summary>
        /// 验证收件人信息
        /// </summary>
        /// <param name="recipients"></param>
        /// <returns>true，表示成功；false 表示失败</returns>
        public static bool ValidateRecipients(string recipients)
        {
            return recipients != null && Regex.IsMatch(recipients, StringValidator);
        }

        /// <summary>
        /// 验证附件地址
        /// </summary>
        /// <param name="attachment">附件绝对路径</param>
        /// <returns>true，表示成功；false 表示失败</returns>
        public static bool ValidateAttachment(string attachment)
        {
            var fileName = Path.GetFileName(attachment);
            var directory = Path.GetDirectoryName(attachment);

            return CheckFileName(fileName) && CheckDirectoryPath(directory);
        }

        #endregion

        #region Static Private Methods

        private static bool CheckFileName(string fileName)
        {
            if (fileName == null)
                return false;

            var invalidChars = Path.GetInvalidFileNameChars();
            return fileName.Any(c => !invalidChars.Contains(c));
        }

        private static bool CheckDirectoryPath(string directoryPath)
        {
            if (directoryPath == null)
                return false;

            var invalidChars = Path.GetInvalidPathChars();
            return directoryPath.Any(c => !invalidChars.Contains(c));
        }

        #endregion
    }
}
