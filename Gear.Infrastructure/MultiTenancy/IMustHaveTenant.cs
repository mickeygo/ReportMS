using System;

namespace Gear.Infrastructure.MultiTenancy
{
    /// <summary>
    /// 表示实现该接口的实体必须存在租户
    /// </summary>
    /// <typeparam name="TKey">租户身份标识类型</typeparam>
    public interface IMustHaveTenant<TKey>
        where TKey : struct
    {
        /// <summary>
        /// 获取实体的租户 ID
        /// </summary>
        TKey TenantId { get; }
    }

    /// <summary>
    /// 表示实现该接口的实体必须存在租户, 租户身份标识类型为 Guid
    /// </summary>
    public interface IMustHaveTenant : IMustHaveTenant<Guid>
    { }
}
