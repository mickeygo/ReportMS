using System;

namespace ReportMS.Framework.Repositories
{
    /// <summary>
    /// 表示实现类是仓储上下文
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 获取聚合仓储上下文的唯一标识
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// 注册一个新的实例到仓储上下文
        /// </summary>
        /// <param name="obj">要添加的对象</param>
        void RegisterNew(object obj);

        /// <summary>
        /// 注册一个要修改的实例到仓储上下文
        /// </summary>
        /// <param name="obj">要修改的对象</param>
        void RegisterModified(object obj);

        /// <summary>
        /// 注册一个要删除的实例到仓储上下文
        /// </summary>
        /// <param name="obj">要删除的对象</param>
        void RegisterDelete(object obj);
    }
}
