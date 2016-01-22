using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 角色实体配置类
    /// </summary>
    internal class RoleEntityTypeConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("RoleId");
            this.HasOptional(p => p.Tenant);

            this.ToTable("RMS_Role");
        }
    }
}
