using System.Data.Entity.ModelConfiguration;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 表示用户角色实体配置类
    /// </summary>
    internal class UserRoleEntityTypeConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleEntityTypeConfiguration()
        {
            this.HasKey(p => p.ID);
            this.Property(p => p.ID).HasColumnName("UserRoleId");
            this.HasRequired(p => p.User);
            this.HasRequired(p => p.Role);

            this.ToTable("RMS_UserRole");
        }
    }
}
