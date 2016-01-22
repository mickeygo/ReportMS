using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// Action Role 实体配置类
    /// </summary>
    internal class ActionRoleEntityTypeConfiguration : EntityTypeConfiguration<ActionRole>
    {
        public ActionRoleEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("ActionRoleId");
            this.Property(p => p.ActionsId).HasColumnName("ActionId");
            this.HasRequired(p => p.Actions);
            this.HasRequired(p => p.Role);

            this.ToTable("RMS_ActionRole");
        }
    }
}
