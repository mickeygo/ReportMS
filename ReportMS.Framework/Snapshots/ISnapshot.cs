using System;

namespace ReportMS.Framework.Snapshots
{
    /// <summary>
    /// 表示实现类为快照
    /// </summary>
    public interface ISnapshot
    {
        /// <summary>
        /// 获取或设置快照生成的时间戳
        /// </summary>
        DateTime Timestamp { get; set; }

        /// <summary>
        /// 获取或设置聚合根标识
        /// </summary>
        Guid AggregateRootID { get; set; }

        /// <summary>
        /// 获取或设置快照版本，通常也为聚合根版本
        /// </summary>
        long Version { get; set; }
    }
}
