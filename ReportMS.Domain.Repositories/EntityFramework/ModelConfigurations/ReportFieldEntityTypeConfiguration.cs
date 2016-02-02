using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// ReportField 实体配置类
    /// </summary>
    internal class ReportFieldEntityTypeConfiguration : EntityTypeConfiguration<ReportField>
    {
        public ReportFieldEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("ReportFieldId");
            this.HasRequired(p => p.Report).WithMany(r => r.Fields);

            this.ToTable("RMS_ReportField");
        }
    }
}
