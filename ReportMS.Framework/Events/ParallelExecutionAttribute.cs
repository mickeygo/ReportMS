using System;

namespace ReportMS.Framework.Events
{
    /// <summary>
    /// 表示这个属性来装饰的事件处理程序将以并行方式处理事件
    /// </summary>
    /// <remarks>此属性只适用于事件处理程序将仅由事件总线使用，事件聚合器或事件调度。将此属性应用于其他类型的类将不影响.</remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ParallelExecutionAttribute : Attribute
    {
    }
}
