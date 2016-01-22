using System.Data.Entity;

namespace Gear.Infrastructure.Repository.EntityFramework
{
    /// <summary>
    /// 表示实现接口的类为 DB 上下文类型
    /// </summary>
    /// <typeparam name="TDbContext">DB 上下文类型</typeparam>
    public interface IDbContext<out TDbContext>
    {
        /// <summary>
        /// 获取 DB 上下文
        /// </summary>
        TDbContext Context { get; }
    }

    /// <summary>
    /// 表示实现接口的类为 DB 上下文类型
    /// </summary>
    public interface IDbContext : IDbContext<DbContext>
    {
    }
}
