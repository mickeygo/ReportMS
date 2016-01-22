using System;
using System.Linq;
using System.Text;
using Gear.Infrastructure.Web.Membership;

namespace Gear.Infrastructure.Web.Mails
{
    /// <summary>
    /// Debug 环境中的 Mail.
    /// 邮件会发给当前登录人和系统管理员
    /// </summary>
    internal class MailWebDebug : MailWeb, IMailWeb
    {
        #region IMailWeb Members

        public void Send(string subject, string body, params string[] tos)
        {
            var debugBody = this.BuildDebugBody(body, tos, null, null);
            this.SendMail(subject, debugBody, this.DebugTos, null, this.DebugBCcs, null);
        }

        public void Send(string subject, string body, string[] tos, string[] attachments)
        {
            var debugBody = this.BuildDebugBody(body, tos, null, null);
            this.SendMail(subject, debugBody, this.DebugTos, null, this.DebugBCcs, attachments);
        }

        public void Send(string subject, string body, string[] tos, string[] ccs, string[] attachments)
        {
            var debugBody = this.BuildDebugBody(body, tos, ccs, null);
            this.SendMail(subject, debugBody, this.DebugTos, null, this.DebugBCcs, attachments);
        }

        public void Send(string subject, string body, string[] tos, string[] ccs, string[] bccs, string[] attachments)
        {
            var debugBody = this.BuildDebugBody(body, tos, ccs, bccs);
            this.SendMail(subject, debugBody, this.DebugTos, null, this.DebugBCcs, attachments);
        }

        #endregion

        #region Private Propeties

        private string[] DebugTos
        {
            get { return this.GetLoginUser(); }
        }

        private string[] DebugBCcs
        {
            get { return MemberManager.GetAdministrators(); }
        }

        #endregion

        #region Private Methods

        private string[] GetLoginUser()
        {
            var loginUser = MemberManager.GetCurrentLoginUser();
            return String.IsNullOrWhiteSpace(loginUser) ? new string[0] : new[] { loginUser };
        }

        private string BuildDebugBody(string body, string[] tos, string[] ccs, string[] bccs)
        {
            var htmlBuilder = new StringBuilder(body);

            htmlBuilder.Append("<div>");
            if (tos != null && tos.Any())
                htmlBuilder.Append(ConvertArrayStringToHtml(tos, "To"));
            if (ccs != null && ccs.Any())
                htmlBuilder.Append(ConvertArrayStringToHtml(ccs, "Cc"));
            if (bccs != null && bccs.Any())
                htmlBuilder.Append(ConvertArrayStringToHtml(bccs, "Bcc"));

            htmlBuilder.Append("</div>");

            return htmlBuilder.ToString();
        }

        private string ConvertArrayStringToHtml(string[] array, string prefix)
        {
            if (array == null || !array.Any())
                return null;

            var htmlBuilder = new StringBuilder("<div>");
            htmlBuilder.AppendFormat("<span style=\"color: #f00;\">{0}</span>: ", prefix);
            foreach (var item in array)
                htmlBuilder.AppendFormat("{0},", item);

            htmlBuilder.Remove(htmlBuilder.Length - 1, 1);
            htmlBuilder.Append("</div>");

            return htmlBuilder.ToString();
        }

        #endregion
    }
}
