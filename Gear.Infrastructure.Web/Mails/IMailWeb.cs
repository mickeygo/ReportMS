namespace Gear.Infrastructure.Web.Mails
{
    /// <summary>
    /// 表示实现此类的接口为邮件
    /// </summary>
    public interface IMailWeb
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件主体，会以 Html 格式显示</param>
        /// <param name="tos">邮件收件人</param>
        void Send(string subject, string body, params string[] tos);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件主体，会以 Html 格式显示</param>
        /// <param name="tos">邮件收件人</param>
        /// <param name="attachments">邮件附件（文件绝对路径）</param>
        void Send(string subject, string body, string[] tos, string[] attachments);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件主体，会以 Html 格式显示</param>
        /// <param name="tos">邮件收件人</param>
        /// <param name="ccs">邮件抄送人</param>
        /// <param name="attachments">邮件附件（文件绝对路径）</param>
        void Send(string subject, string body, string[] tos, string[] ccs, string[] attachments);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件主体，会以 Html 格式显示</param>
        /// <param name="tos">邮件收件人</param>
        /// <param name="ccs">邮件抄送人</param>
        /// <param name="bccs">邮件密送人</param>
        /// <param name="attachments">邮件附件（文件绝对路径）</param>
        void Send(string subject, string body, string[] tos, string[] ccs, string[] bccs, string[] attachments);
    }
}
