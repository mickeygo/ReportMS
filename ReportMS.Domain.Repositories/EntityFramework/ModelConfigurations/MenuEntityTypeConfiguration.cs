using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 菜单实体配置类
    /// </summary>
    internal class MenuEntityTypeConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("MenuId");
            this.Property(p => p.ActionsId).HasColumnName("ActionId");
            this.HasOptional(p => p.Actions);

            this.ToTable("RMS_Menu");
        }
    }
}
