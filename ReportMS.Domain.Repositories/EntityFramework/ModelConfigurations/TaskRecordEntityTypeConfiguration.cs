using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 主题任务执行记录实体配置
    /// </summary>
    internal class TaskRecordEntityTypeConfiguration : EntityTypeConfiguration<TaskRecord>
    {
        public TaskRecordEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("TaskRecordId");

            this.ToTable("RMS_TaskRecord");
        }
    }
}
