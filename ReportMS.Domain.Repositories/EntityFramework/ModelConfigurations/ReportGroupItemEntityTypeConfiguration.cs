using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 报表分组项的实体配置类
    /// </summary>
    internal class ReportGroupItemEntityTypeConfiguration : EntityTypeConfiguration<ReportGroupItem>
    {
        public ReportGroupItemEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("ReportGroupItemId");
            this.HasRequired(p => p.ReportGroup);

            this.ToTable("RMS_ReportGroupItem");
        }
    }
}
