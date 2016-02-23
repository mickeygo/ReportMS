using System.Data.Entity;
using EntityFramework.DynamicFilters;
using Gear.Infrastructure.Repositories;

namespace Gear.Infrastructure.Repository.EntityFramework.Extensions
{
    /// <summary>
    /// DbContext 扩展类
    /// </summary>
    public static class DbContextExtension
    {
        /// <summary>
        /// 启用软删除。
        /// 对实现了<see cref="ISoftDelete"/>接口的实体，会筛选出 Enabled 为 true 的实体
        /// </summary>
        /// <param name="modelBuilder"><see cref="DbModelBuilder"/>, 用于将 CLR class集映射到一个 database schema</param>
        public static void EnableSoftDelete(this DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("SoftDelete_Enabled", (ISoftDelete d) => d.Enabled, true);
        }
    }
}
