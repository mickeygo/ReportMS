using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.ReportModule.ReportProfileAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 报表配置的实体配置类
    /// </summary>
    internal class ReportProfileEntityTypeConfiguration : EntityTypeConfiguration<ReportProfile>
    {
        public ReportProfileEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("ReportProfileId");
            this.HasRequired(p => p.Report);

            this.ToTable("RMS_ReportProfile");
        }
    }
}
