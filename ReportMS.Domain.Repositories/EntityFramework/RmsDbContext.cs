using System.Data.Entity;
using Gear.Infrastructure.Repository.EntityFramework;
using Gear.Infrastructure.Repository.EntityFramework.Extensions;
using ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations;

namespace ReportMS.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// 表示 RMS 系统数据访问上下文，此类不可继承
    /// </summary>
    public sealed class RmsDbContext : DbContext, IDbContext
    {
        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>RmsDbContext</c>实例
        /// </summary>
        /// <param name="nameOrConnectionString">数据库的连接名</param>
        public RmsDbContext(string nameOrConnectionString)
            : base(EnvironmentConnection.GetDbConnectionName(nameOrConnectionString))
        { }

        #endregion

        #region IDbContext<DbContext> Members

        /// <summary>
        /// 获取 DB 上下文
        /// </summary>
        public DbContext Context
        {
            get { return this; }
        }

        #endregion

        #region Override Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Account
            modelBuilder.Configurations.Add(new TenantEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new RoleEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ActionEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new MenuEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserRoleEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new MenuRoleEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ActionRoleEntityTypeConfiguration());

            // Report
            modelBuilder.Configurations.Add(new RdbmsEntityTypeConfiguration());

            modelBuilder.Configurations.Add(new ReportEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ReportFieldEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ReportGroupEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ReportGroupItemEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ReportProfileEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ReportProfileFieldEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ReportGroupRoleEntityTypeConfiguration());

            // Subscribe
            modelBuilder.Configurations.Add(new TopicEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new AttachmentTopicEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new TopicTaskEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new SubscriberEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new TaskRecordEntityTypeConfiguration());

            // DynamicFilter
            modelBuilder.EnableSoftDelete();

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
