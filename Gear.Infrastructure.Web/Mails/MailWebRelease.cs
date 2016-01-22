namespace Gear.Infrastructure.Web.Mails
{
    /// <summary>
    /// Release 环境中发送邮件
    /// </summary>
    internal class MailWebRelease : MailWeb, IMailWeb
    {
        #region IMailWeb Members

        public void Send(string subject, string body, params string[] tos)
        {
            this.SendMail(subject, body, tos, null, null, null);
        }

        public void Send(string subject, string body, string[] tos, string[] attachments)
        {
            this.SendMail(subject, body, tos, null, null, attachments);
        }

        public void Send(string subject, string body, string[] tos, string[] ccs, string[] attachments)
        {
            this.SendMail(subject, body, tos, ccs, null, attachments);
        }

        public void Send(string subject, string body, string[] tos, string[] ccs, string[] bccs, string[] attachments)
        {
            this.SendMail(subject, body, tos, ccs, bccs, attachments);
        }

        #endregion
    }
}
