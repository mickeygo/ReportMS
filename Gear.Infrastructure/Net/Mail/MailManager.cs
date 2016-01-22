using System;
using System.Linq;
using System.Net.Mail;
using Gear.Infrastructure.Configurations.Fluent;

namespace Gear.Infrastructure.Net.Mail
{
    /// <summary>
    /// 邮件管理类
    /// </summary>
    public class MailManager
    {
        #region Private Fields

        private readonly string _host;
        private readonly MailMessageManager _mailMessage = new MailMessageManager();

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>MailManager</c>实例
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件主体，会以 Html 格式显示</param>
        /// <param name="tos">邮件收件人</param>
        public MailManager(string subject, string body, params string[] tos)
            : this(subject, body, tos, null)
        {
        }

        /// <summary>
        /// 初始化一个新的<c>MailManager</c>实例
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件主体，会以 Html 格式显示</param>
        /// <param name="tos">邮件收件人</param>
        /// <param name="attachments">邮件附件（文件绝对路径）</param>
        public MailManager(string subject, string body, string[] tos, string[] attachments)
            : this(subject, body, tos, null, attachments)
        {
        }

        /// <summary>
        /// 初始化一个新的<c>MailManager</c>实例
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件主体，会以 Html 格式显示</param>
        /// <param name="tos">邮件收件人</param>
        /// <param name="ccs">邮件抄送人</param>
        /// <param name="attachments">邮件附件（文件绝对路径）</param>
        public MailManager(string subject, string body, string[] tos, string[] ccs, string[] attachments)
            : this(subject, body, tos, ccs, null, attachments)
        {
        }

        /// <summary>
        /// 初始化一个新的<c>MailManager</c>实例
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件主体，会以 Html 格式显示</param>
        /// <param name="tos">邮件收件人</param>
        /// <param name="ccs">邮件抄送人</param>
        /// <param name="bccs">邮件密送人</param>
        /// <param name="attachments">邮件附件（文件绝对路径）</param>
        public MailManager(string subject, string body, string[] tos, string[] ccs, string[] bccs, string[] attachments)
            : this()
        {
            this._mailMessage.SetSubject(subject);
            this._mailMessage.SetBody(body);
            this.SetRecipientsAndAttachments(tos, ccs, bccs, attachments);
        }

        private MailManager()
        {
            this._host = MailConfigurator.Default.EmailClientElement.Host;
            var from = MailConfigurator.Default.EmailClientElement.Sender;
            var displayFrom = MailConfigurator.Default.EmailClientElement.DisplayName;

            this._mailMessage.SetFrom(from, displayFrom);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 发送邮件
        /// </summary>
        public void Send()
        {
            SmtpClient client = null;

            try
            {
                client = new SmtpClient(this._host);
                var message = this._mailMessage.GetMailMessage();
                client.Send(message);
            }
            finally
            {
                this._mailMessage.Dispose();
                if (client != null)
                    client.Dispose();
            }
        }

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        public void SendAsync()
        {
            SmtpClient client = null;

            try
            {
                client = new SmtpClient(this._host);
                var message = this._mailMessage.GetMailMessage();
                client.SendAsync(message, null);
            }
            finally
            {
                this._mailMessage.Dispose();
                if (client != null)
                    client.Dispose();
            }
        }

        #endregion

        #region Private Methods

        private void SetRecipientsAndAttachments(string[] tos, string[] ccs, string[] bccs, string[] attachments)
        {
            var hasAnyRecipients = (tos != null && tos.Any(MailAudit.ValidateRecipients))
                                   || (ccs != null && ccs.Any(MailAudit.ValidateRecipients))
                                   || (bccs != null && bccs.Any(MailAudit.ValidateRecipients));

            if (!hasAnyRecipients)
                throw new InvalidOperationException("The recipients (to, cc, bcc) is all empty or all invalid.");

            if (tos != null && tos.Any())
                this._mailMessage.AddTos(tos);

            if (ccs != null && ccs.Any())
                this._mailMessage.AddCcs(ccs);

            if (bccs != null && bccs.Any())
                this._mailMessage.AddBccs(bccs);

            if (attachments != null && attachments.Any())
                this._mailMessage.AddAttachments(attachments);
        }

        #endregion
    }
}
