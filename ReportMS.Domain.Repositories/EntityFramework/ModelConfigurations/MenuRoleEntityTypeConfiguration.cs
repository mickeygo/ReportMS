using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 菜单角色实体配置类
    /// </summary>
    internal class MenuRoleEntityTypeConfiguration : EntityTypeConfiguration<MenuRole>
    {
        public MenuRoleEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("MenuRoleId");
            this.HasRequired(p => p.Menu);
            this.HasRequired(p => p.Role);

            this.ToTable("RMS_MenuRole");
        }
    }
}
