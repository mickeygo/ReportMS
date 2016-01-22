namespace Gear.Infrastructure.Web.Mails
{
    /// <summary>
    /// 邮件工厂
    /// </summary>
    public static class MailFactory
    {
        #region Public Proterties

        /// <summary>
        /// 默认配置的 Web 邮件
        /// </summary>
        public static IMailWeb Default
        {
            get { return CreateMail(); }
        }
        
        #endregion

        #region Private Methods

        private static IMailWeb CreateMail()
        {
#if DEBUG
            return new MailWebDebug();
#else
            return new MailWebRelease();
#endif
        }

        #endregion
    }
}
