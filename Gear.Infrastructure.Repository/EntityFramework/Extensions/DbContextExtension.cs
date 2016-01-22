using System.Data.Entity;
using EntityFramework.DynamicFilters;
using Gear.Infrastructure.Repositories;

namespace Gear.Infrastructure.Repository.EntityFramework.Extensions
{
    /// <summary>
    /// DB 扩展类
    /// </summary>
    public static class DbContextExtension
    {
        /// <summary>
        /// 启用软删除，会过滤掉实现了<see cref="ISoftDelete"/>接口的实体
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder</param>
        public static void EnableSoftDelete(this DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("SoftDelete_Enabled", (ISoftDelete d) => d.Enabled, true);
        }
    }
}
