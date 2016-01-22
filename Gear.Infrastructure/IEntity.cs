using System;

namespace Gear.Infrastructure
{
    /// <summary>
    /// 表示实现接口类是领域实体
    /// </summary>
    /// <typeparam name="TKey">实体对象标识类型</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 获取或设置领域实体主键
        /// </summary>
        TKey ID { get; set; }
    }

    /// <summary>
    /// 表示实现接口类是领域实体，实体主键类型为 Guid
    /// </summary>
    public interface IEntity : IEntity<Guid>
    { }
}
