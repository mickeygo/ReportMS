using System.Configuration;

namespace Gear.Infrastructure.Configurations
{
    /// <summary>
    /// 系统管理节点配置
    /// </summary>
    public class SystemAdminSectionConfiguration : ConfigurationSection
    {
        #region Properties

        internal const string SystemAdmiPropertyName = "systemAdmin";

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取系统管理元素配置信息，节点为 systemAdmin
        /// </summary>
        [ConfigurationProperty(SystemAdmiPropertyName, IsRequired = true)]
        public SystemAdminElementConfiguration SystemAdminElement
        {
            get { return (SystemAdminElementConfiguration) this[SystemAdmiPropertyName]; }
            set { this[SystemAdmiPropertyName] = value; }
        }

        #endregion
    }

    /// <summary>
    /// 系统管理元素配置
    /// </summary>
    public class SystemAdminElementConfiguration : ConfigurationElement
    {
        #region Properties

        internal const string AdministratorPropertyName = "administrator";

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取系统的管理人员信息
        /// </summary>
        [ConfigurationProperty(AdministratorPropertyName, IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!#$%^&*()[]{}/'\"|\\")]
        public string Administrator
        {
            get { return (string) this[AdministratorPropertyName]; }
            set { this[AdministratorPropertyName] = value; }
        }

        #endregion
    }
}
