using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.ReportModule.ReportProfileAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    ///  分组明细中包含的字段实体配置类
    /// </summary>
    internal class ReportProfileFieldEntityTypeConfiguration : EntityTypeConfiguration<ReportProfileField>
    {
        public ReportProfileFieldEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("ReportProfileFieldId");
            this.HasRequired(p => p.ReportProfile);

            this.ToTable("RMS_ReportProfileField");
        }
    }
}
