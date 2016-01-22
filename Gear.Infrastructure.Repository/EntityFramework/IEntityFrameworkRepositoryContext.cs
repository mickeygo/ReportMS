using System.Data.Entity;
using Gear.Infrastructure.Repositories;

namespace Gear.Infrastructure.Repository.EntityFramework
{
    /// <summary>
    /// 表示继承于该接口的类型，是由 Microsoft Entity Framework 支持的一种仓储上下文的实现。
    /// </summary>
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        /// <summary>
        /// 获取当前仓储上下文所使用的Entity Framework的<see cref="DbContext"/>实例。
        /// </summary>
        DbContext Context { get; }
    }
}
