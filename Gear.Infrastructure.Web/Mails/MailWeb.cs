using System;
using System.Collections.Generic;
using System.IO;
using Gear.Infrastructure.Net.Mail;

namespace Gear.Infrastructure.Web.Mails
{
    /// <summary>
    /// Web 中 Mail 基类
    /// </summary>
    internal abstract class MailWeb
    {
        #region Protected Methods

        protected void SendMail(string subject, string body, string[] tos, string[] ccs, string[] bccs, string[] attachments)
        {
            var manager = new MailManager(subject, body, tos, ccs, bccs, attachments);
            manager.Send();
        }

        protected void SendMail(string subject, string body, string[] tos, string[] ccs, string[] bccs, IEnumerable<Tuple<Stream, string>> attachments)
        {
            var manager = new MailManager(subject, body, tos, ccs, bccs, attachments);
            manager.Send();
        }

        #endregion 
    }
}
