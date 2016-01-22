using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.TenantModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 租户实体配置类
    /// </summary>
    internal class TenantEntityTypeConfiguration : EntityTypeConfiguration<Tenant>
    {
        public TenantEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("TenantId");

            this.ToTable("RMS_Tenant");
        }
    }
}
