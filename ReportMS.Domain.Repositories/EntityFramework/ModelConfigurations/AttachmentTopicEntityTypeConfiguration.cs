using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// Attachment 主题实体对象配置类
    /// </summary>
    internal class AttachmentTopicEntityTypeConfiguration : EntityTypeConfiguration<AttachmentTopic>
    {
        public AttachmentTopicEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("TopicId");

            this.ToTable("RMS_AttachmentTopic");
        }
    }
}
