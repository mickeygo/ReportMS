using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// Action 实体配置类
    /// </summary>
    internal class ActionEntityTypeConfiguration : EntityTypeConfiguration<Actions>
    {
        public ActionEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("ActionId");

            this.ToTable("RMS_Action");
        }
    }
}
