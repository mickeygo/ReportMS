using System;

namespace Gear.Infrastructure.Web.Attributes
{
    /// <summary>
    /// 允许已认证的用户访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AllowAuthenticatedAttribute : Attribute
    {
    }
}
