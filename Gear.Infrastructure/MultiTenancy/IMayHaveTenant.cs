using System;

namespace Gear.Infrastructure.MultiTenancy
{
    /// <summary>
    /// 表示实现该接口的实体可能会有租户
    /// </summary>
    /// <typeparam name="TKey">租户身份标识类型</typeparam>
    public interface IMayHaveTenant<TKey>
        where TKey: struct
    {
        /// <summary>
        /// 获取实体的租户 ID
        /// </summary>
        TKey? TenantId { get; }
    }

    /// <summary>
    /// 表示实现该接口的实体可能会有租户, 租户身份标识类型为 Guid
    /// </summary>
    public interface IMayHaveTenant : IMayHaveTenant<Guid>
    { }
}
