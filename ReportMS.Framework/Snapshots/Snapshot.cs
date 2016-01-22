using System;

namespace ReportMS.Framework.Snapshots
{
    /// <summary>
    /// 快照的基类
    /// </summary>
    [Serializable]
    public abstract class Snapshot : ISnapshot
    {
        #region ISnapshot Members

        /// <summary>
        /// 获取或设置快照生成的时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 获取或设置聚合根标识
        /// </summary>
        public Guid AggregateRootID { get; set; }

        /// <summary>
        /// 获取或设置快照版本，通常也为聚合根版本
        /// </summary>
        public long Version { get; set; }

        #endregion
    }
}
