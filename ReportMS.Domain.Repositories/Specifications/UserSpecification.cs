using System;
using Gear.Infrastructure.Specifications;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.Specifications
{
    /// <summary>
    /// 与 User 有关的规约
    /// </summary>
    public static class UserSpecification
    {
        /// <summary>
        /// 根据指定的用户名查找用户
        /// </summary>
        /// <param name="userName">要查找用户名</param>
        /// <returns>规约</returns>
        public static ISpecification<User> FindUser(string userName)
        {
            return Specification<User>.Eval(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
