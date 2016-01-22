using System.Data.Entity;
using Gear.Infrastructure;

namespace ReportMS.Domain.Repositories.EntityFramework.DbContextInitializer
{
    /// <summary>
    /// RMS 数据库初始化工作
    /// </summary>
    public sealed class RmsDbContextInitializer : IApplicationStartup
    {
        /// <summary>
        /// 执行对数据库的初始化操作。
        /// </summary>
        public void Initialize()
        {
            Database.SetInitializer<RmsDbContext>(null);
        }
    }
}
