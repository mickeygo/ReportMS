using System;

namespace Gear.Infrastructure
{
    /// <summary>
    /// 表示实现类是领域聚合根
    /// </summary>
    /// <typeparam name="TKey">聚合根标识类型</typeparam>
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    { }

    /// <summary>
    /// 表示实现类是领域聚合根
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<Guid>, IEntity
    { }
}
