namespace Gear.Infrastructure.Configurations.Fluent
{
    /// <summary>
    /// 邮件配置器
    /// </summary>
    public class MailConfigurator : Configurator<EmailClientSectionConfiguration>
    {
        #region Private Properties

        private static EmailClientSectionConfiguration emailConfiguration;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个<c>MailConfigurator</c>实例。
        /// 注意：默认节点为 gearEmail
        /// </summary>
        public MailConfigurator()
            : this(ConfigurationOptions.Email)
        {
        }

        /// <summary>
        ///  初始化一个<c>MailConfigurator</c>实例。
        /// </summary>
        /// <param name="section">要配置的节点名称</param>
        public MailConfigurator(string section)
            : base(section)
        {
        }

        #endregion

        #region Static Public Methods

        /// <summary>
        /// 获取默认的邮件配置信息。
        /// 默认节点为 gearMail
        /// </summary>
        public static EmailClientSectionConfiguration Default
        {
            get
            {
                if (emailConfiguration == null)
                {
                    var configurator = new MailConfigurator();
                    emailConfiguration = configurator.GetConfiguration();
                }
                return emailConfiguration;
            }
        }

        #endregion
    }
}
