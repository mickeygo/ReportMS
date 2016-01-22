using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 报表组角色实体配置类
    /// </summary>
    internal class ReportGroupRoleEntityTypeConfiguration : EntityTypeConfiguration<ReportGroupRole>
    {
        public ReportGroupRoleEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("ReportGroupRoleId");
            this.HasRequired(p => p.ReportGroup);
            this.HasRequired(p => p.Role);

            this.ToTable("RMS_ReportGroupRole");
        }
    }
}
