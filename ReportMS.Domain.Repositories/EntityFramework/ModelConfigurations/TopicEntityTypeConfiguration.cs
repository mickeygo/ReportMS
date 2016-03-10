using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 主题实体配置类
    /// </summary>
    internal class TopicEntityTypeConfiguration : EntityTypeConfiguration<Topic>
    {
        public TopicEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("TopicId");

            this.ToTable("RMS_Topic");
        }
    }
}
