using System;

namespace Gear.Infrastructure.Events
{
    /// <summary>
    /// 表示应用此属性的事件处理者将以异步执行的方式处理事件
    /// </summary>
    /// <remarks>此属性仅仅能被用于消息处理者，并且将仅仅能被消息总线或消息派发者使用。在其他地方使用此属性无效</remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ParallelExecutionAttribute : Attribute
    {
    }
}
