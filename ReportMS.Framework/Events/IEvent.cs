using System;

namespace ReportMS.Framework.Events
{
    /// <summary>
    /// 表示实现类是事件
    /// </summary>
    public interface IEvent : IEntity
    {
        /// <summary>
        /// 获取或设置事件产生的时间戳
        /// </summary>
        DateTime Timestamp { get; set; }

        /// <summary>
        /// 获取或设置事件的程序集限定名
        /// </summary>
        string AssemblyQualifiedEventType { get; set; }
    }
}
