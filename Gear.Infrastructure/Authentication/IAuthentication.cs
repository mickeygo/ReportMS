using System;
using System.Security.Principal;

namespace Gear.Infrastructure.Authentication
{
    /// <summary>
    /// 表示实现类为身份验证
    /// </summary>
    public interface IAuthentication : IPrincipal
    {
        /// <summary>
        /// 获取用户的身份标识. 若不存在，则为 null
        /// </summary>
        Guid? NameIdentifier { get; }

        /// <summary>
        /// 获取当前用户的 Email
        /// </summary>
        string Email { get; }

        /// <summary>
        /// 获取当前用户拥有的角色
        /// </summary>
        string[] Roles { get; }
    }
}
