using System.Configuration;

namespace Gear.Infrastructure.Configurations
{
    /// <summary>
    /// Email Client 节点配置类
    /// </summary>
    public class EmailClientSectionConfiguration : ConfigurationSection
    {
        #region Properties

        internal const string EmailClientPropertyName = "emailClient";

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取或设置邮件配置元素，元素节点名为 emailClient
        /// </summary>
        [ConfigurationProperty(EmailClientPropertyName, IsRequired = true)]
        public EmailClientElementConfiguration EmailClientElement
        {
            get { return (EmailClientElementConfiguration) this[EmailClientPropertyName]; }
            set { this[EmailClientPropertyName] = value; }
        }

        #endregion
    }

    /// <summary>
    /// Email Client 元素配置类
    /// </summary>
    public class EmailClientElementConfiguration : ConfigurationElement
    {
        #region Properties

        internal const string HostPropertyName = "host";
        internal const string PortPropertyName = "port";
        internal const string UserNamePropertyName = "userName";
        internal const string PasswordPropertyName = "password";
        internal const string EnableSslPropertyName = "enableSsl";
        internal const string SenderPropertyName = "sender";
        internal const string SenderDisplayPropertyName = "displayName";

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取或设置邮件服务器
        /// </summary>
        [ConfigurationProperty(HostPropertyName, DefaultValue = "", IsRequired = true, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MaxLength = 60)]
        public string Host
        {
            get { return (string) this[HostPropertyName]; }
            set { this[HostPropertyName] = value; }
        }

        /// <summary>
        /// 获取或设置邮件端口，默认为 25
        /// </summary>
        [ConfigurationProperty(PortPropertyName, DefaultValue = 25, IsRequired = false, IsKey = false)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 99999, MinValue = 1)]
        public int Port
        {
            get { return (int) this[PortPropertyName]; }
            set { this[PortPropertyName] = value; }
        }

        /// <summary>
        /// 获取或设置邮件用户，默认为空
        /// </summary>
        [ConfigurationProperty(UserNamePropertyName, DefaultValue = "", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!#$%^&*()[]{}/;'\"|\\", MaxLength = 60)]
        public string UserName
        {
            get { return (string) this[UserNamePropertyName]; }
            set { this[UserNamePropertyName] = value; }
        }

        /// <summary>
        /// 获取或设置邮件用户密码，默认为空
        /// </summary>
        [ConfigurationProperty(PasswordPropertyName, DefaultValue = "", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!^()[]{}/;'\"|\\", MaxLength = 60)]
        public string Password
        {
            get { return (string) this[PasswordPropertyName]; }
            set { this[PasswordPropertyName] = value; }
        }

        /// <summary>
        /// 获取或设置一个<see cref="System.Boolean"/>值，表示是否启用 SSL 传输协议，默认为 false
        /// </summary>
        [ConfigurationProperty(EnableSslPropertyName, DefaultValue = false, IsRequired = false, IsKey = false)]
        public bool EnableSsl
        {
            get { return (bool) this[EnableSslPropertyName]; }
            set { this[EnableSslPropertyName] = value; }
        }

        /// <summary>
        /// 获取或设置一个值，表示邮件发件人
        /// </summary>
        [ConfigurationProperty(SenderPropertyName, DefaultValue = "", IsRequired = true, IsKey = false)]
        public string Sender
        {
            get { return (string) this[SenderPropertyName]; }
            set { this[SenderPropertyName] = value; }
        }

        /// <summary>
        /// 获取或设置一个值，表示邮件发件人显示的匿名名称
        /// </summary>
        [ConfigurationProperty(SenderDisplayPropertyName, DefaultValue = "", IsRequired = false, IsKey = false)]
        public string DisplayName
        {
            get { return (string) this[SenderDisplayPropertyName]; }
            set { this[SenderDisplayPropertyName] = value; }
        }

        #endregion
    }
}
