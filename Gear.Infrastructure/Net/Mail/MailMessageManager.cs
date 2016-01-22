using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Gear.Infrastructure.Net.Mail
{
    /// <summary>
    /// 邮件消息管理类
    /// </summary>
    internal class MailMessageManager : IDisposable
    {
        private readonly Encoding defaultEncoding = Encoding.UTF8;
        private readonly MailMessage mailMessage;

        #region Ctor

        /// <summary>
        /// 初始化一个<c>MailMessageManager</c>实例
        /// </summary>
        public MailMessageManager()
        {
            this.mailMessage = new MailMessage();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 设置邮件发信人信息
        /// </summary>
        /// <param name="from">发信人地址</param>
        /// <param name="displayName">发信人显示名</param>
        public void SetFrom(string from, string displayName = null)
        {
            if (!MailAudit.ValidateRecipients(from))
                throw new ArgumentException("The mail from is invalid.");
            this.mailMessage.From = new MailAddress(from, displayName);
        }

        /// <summary>
        /// 设置邮件消息主题
        /// </summary>
        /// <param name="subject">要设置的邮件消息主题</param>
        public void SetSubject(string subject)
        {
            this.mailMessage.Subject = subject;
            this.mailMessage.SubjectEncoding = this.defaultEncoding;
        }

        /// <summary>
        /// 设置邮件消息主题
        /// </summary>
        /// <param name="body">要设置的邮件消息主体</param>
        /// <param name="isBodyHtml">主体是否是 Html 类型，默认为 true</param>
        public void SetBody(string body, bool isBodyHtml = true)
        {
            this.mailMessage.Body = body;
            this.mailMessage.SubjectEncoding = this.defaultEncoding;
            this.mailMessage.IsBodyHtml = isBodyHtml;
        }

        /// <summary>
        /// 添加收件人。
        /// 若收件人不符合 mail 格式，将会被移除
        /// </summary>
        /// <param name="to">要添加的收件人</param>
        public void AddTo(string to)
        {
            if (!MailAudit.ValidateRecipients(to))
                return;

            this.mailMessage.To.Add(new MailAddress(to));
        }

        /// <summary>
        /// 添加收件人。
        /// 若收件人不符合 mail 格式，将会被移除
        /// </summary>
        /// <param name="tos">要添加的收件人集合</param>
        public void AddTos(string[] tos)
        {
            foreach (var to in tos)
                this.AddTo(to);
        }

        /// <summary>
        /// 添加抄送人。
        /// 若抄送人不符合 mail 格式，将会被移除
        /// </summary>
        /// <param name="cc">要添加的抄送人</param>
        public void AddCc(string cc)
        {
            if (!MailAudit.ValidateRecipients(cc))
                return;

            this.mailMessage.CC.Add(new MailAddress(cc));
        }

        /// <summary>
        /// 添加抄送人。
        /// 若抄送人不符合 mail 格式，将会被移除
        /// </summary>
        /// <param name="ccs">要添加的抄送人集合</param>
        public void AddCcs(string[] ccs)
        {
            foreach (var cc in ccs)
                this.AddCc(cc);
        }

        /// <summary>
        /// 添加密送人。
        /// 若密送不符合 mail 格式，将会被移除
        /// </summary>
        /// <param name="bcc">要添加的密送人</param>
        public void AddBcc(string bcc)
        {
            if (!MailAudit.ValidateRecipients(bcc))
                return;

            this.mailMessage.Bcc.Add(new MailAddress(bcc));
        }

        /// <summary>
        /// 添加密送人。
        /// 若密送不符合 mail 格式，将会被移除
        /// </summary>
        /// <param name="bccs">要添加的密送人集合</param>
        public void AddBccs(string[] bccs)
        {
            foreach (var bcc in bccs)
                this.AddBcc(bcc);
        }

        /// <summary>
        /// 添加附件。
        /// 若附件路径不合法，将会被移除
        /// </summary>
        /// <param name="attachment">要添加的附件的绝对路径</param>
        public void AddAttachment(string attachment)
        {
            if (!MailAudit.ValidateAttachment(attachment))
                return;

            this.mailMessage.Attachments.Add(new Attachment(attachment, MediaTypeNames.Application.Octet));
        }

        /// <summary>
        /// 添加附件。
        /// 若附件路径不合法，将会被移除
        /// </summary>
        /// <param name="attachments">要添加的附件的绝对路径集合</param>
        public void AddAttachments(string[] attachments)
        {
            foreach (var attachment in attachments)
                this.AddAttachment(attachment);
        }

        /// <summary>
        /// 获取邮件消息
        /// </summary>
        /// <returns><c>MailMessage</c>邮件消息</returns>
        public MailMessage GetMailMessage()
        {
            return this.mailMessage;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.mailMessage.Dispose();
        }

        #endregion
    }
}
