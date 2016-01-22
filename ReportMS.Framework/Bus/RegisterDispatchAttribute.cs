using System;

namespace ReportMS.Framework.Bus
{
    /// <summary>
    /// 表示在消息分发器中注册的装饰接口的实例
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class RegisterDispatchAttribute : Attribute
    {
    }
}
