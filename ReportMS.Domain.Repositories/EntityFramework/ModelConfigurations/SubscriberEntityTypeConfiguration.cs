using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 订阅者配置信息
    /// </summary>
    internal class SubscriberEntityTypeConfiguration : EntityTypeConfiguration<Subscriber>
    {
        public SubscriberEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("SubscriberId");
            this.HasRequired(p => p.Topic);

            this.ToTable("RMS_Subscriber");
        }
    }
}
