using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 主题任务实体配置类
    /// </summary>
    internal class TopicTaskEntityTypeConfiguration : EntityTypeConfiguration<TopicTask>
    {
        public TopicTaskEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("TopicTaskId");
            this.HasRequired(p => p.Topic);

            this.ToTable("RMS_TopicTask");
        }
    }
}
