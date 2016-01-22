using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// User 实体配置
    /// </summary>
    internal class UserEntityTypeConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("UserId");

            this.ToTable("RMS_User");
        }
    }
}
