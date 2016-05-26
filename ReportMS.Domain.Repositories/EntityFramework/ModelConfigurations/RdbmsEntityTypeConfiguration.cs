using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.ReportModule.RdbmsAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 关系型数据库实体配置对象
    /// </summary>
    internal class RdbmsEntityTypeConfiguration : EntityTypeConfiguration<Rdbms>
    {
        public RdbmsEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("RdbmsId");

            this.ToTable("RMS_Rdbms");
        }
    }
}
