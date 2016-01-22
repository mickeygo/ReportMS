using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 报表分组实体配置类
    /// </summary>
    public class ReportGroupEntityTypeConfiguration : EntityTypeConfiguration<ReportGroup>
    {
        public ReportGroupEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("ReportGroupId");

            this.ToTable("RMS_ReportGroup");
        }
    }
}
