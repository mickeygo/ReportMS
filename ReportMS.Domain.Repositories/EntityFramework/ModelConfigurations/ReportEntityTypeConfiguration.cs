using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// Report 实体配置类
    /// </summary>
    internal class ReportEntityTypeConfiguration : EntityTypeConfiguration<Report>
    {
        public ReportEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("ReportId");
            this.HasRequired(p => p.Rdbms);

            this.ToTable("RMS_Report");
        }
    }
}
