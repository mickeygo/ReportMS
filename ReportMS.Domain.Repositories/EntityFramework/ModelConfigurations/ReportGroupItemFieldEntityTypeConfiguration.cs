using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    ///  分组明细中包含的字段实体配置类
    /// </summary>
    internal class ReportGroupItemFieldEntityTypeConfiguration : EntityTypeConfiguration<ReportGroupItemField>
    {
        public ReportGroupItemFieldEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("ReportGroupItemFieldId");
            this.HasRequired(p => p.ReportGroupItem);

            this.ToTable("RMS_ReportGroupItemField");
        }
    }
}
