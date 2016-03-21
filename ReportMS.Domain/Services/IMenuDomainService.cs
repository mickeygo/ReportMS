using System;
using Gear.Infrastructure.Services.DomainServices;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories;

namespace ReportMS.Domain.Services
{
    /// <summary>
    /// 表示实现此接口的类为菜单的领域服务
    /// </summary>
    public interface IMenuDomainService : IDomainService
    {
        /// <summary>
        /// 添加或更新菜单。
        /// Repository 不会执行 Commit 动作.
        /// </summary>
        /// <param name="menuRepository">菜单仓储</param>
        /// <param name="menu">要添加或更新的菜单</param>
        void AddOrUpdateMenu(IMenuRepository menuRepository, Menu menu);
    }
}
