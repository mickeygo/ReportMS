using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure.Specifications;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories;

namespace ReportMS.Domain.Services.Implement
{
    /// <summary>
    /// 表示菜单的领域服务
    /// </summary>
    public class MenuDomainService : IMenuDomainService
    {
        #region IMenuDomainService Members

        public void AddOrUpdateMenu(IMenuRepository menuRepository, Menu menu)
        {
            this.InsertOrUpdateMenu(menuRepository, menu.Sort, menu, menu.Level, menu.ParentId);
        }

        #endregion

        #region Private Methods

        private void InsertOrUpdateMenu(IMenuRepository menuRepository, int index, Menu menu, MenuLevel level, Guid? parentId)
        {
            // sort start with 1

            // a1, check the paramters
            //  1, check the level and parentId
            //      1a, when level = parent, the parentId = null
            //      2a, when level != parent and the parentId = null, throw exception.
            // a2, check the index
            //  1, index > 0

            // a1, when level = parent
            //  1, find all menus that level = parent
            //      1a, if the index > menus count, set the sort = (countOfMenu + 1)
            //      1b, if the index <= menus count, set the sort = (countOfMenu + 1), then set the sort that large then index add one
            // a2, when level = child
            //  1, find all menus that parentId = input parentId
            //      1a, when the index > menus count, set the sort = (countOfMenu + 1)
            //      1b, when the index <= menus count, set the sort = (countOfMenu + 1), then set the sort that large then index add one

            ISpecification<Menu> spec = null;
            var existMenu = menuRepository.Exist(Specification<Menu>.Eval(m => m.ID == menu.ID));

            if (level == MenuLevel.Parent)
            {
                spec = Specification<Menu>.Eval(m => m.Level == MenuLevel.Parent);
            }

            if (level == MenuLevel.Children)
            {
                if (!parentId.HasValue)
                    throw new ArgumentNullException("parentId", "When menuLevel is children, the parentId must be not null.");

                spec = Specification<Menu>.Eval(m => m.Level == MenuLevel.Children && m.ParentId == parentId);
            }

            var menus = this.GetMenus(menuRepository, spec, menu.ID, existMenu);
            
            // if the menu not exist, add the menu (the menu has been atteched).
            if (!existMenu)
            {
                var sort = this.InsertSortMenu(menus, index);
                menu.SetSort(sort);
                menuRepository.Add(menu);
            }
            else
            {
                var sort = this.MoveSortMenu(menus, menu.Sort, index);
                menu.SetSort(sort);
                menuRepository.Update(menu);
                foreach (var item in menus)
                    menuRepository.Update(item);
            }
        }

        private int InsertSortMenu(List<Menu> menus, int index)
        {
            var sort = 1;
            if (!menus.Any())
                return sort;

            var maxSort = menus.Max(m => m.Sort); // do not use the collection count.
            if (index > maxSort)
            {
                sort = maxSort + 1;
            }
            else
            {
                sort = index;
                menus.FindAll(m => m.Sort >= maxSort).ForEach(s =>
                {
                    s.IncreaseSort();
                });
            }

            return sort;
        }

        private int MoveSortMenu(List<Menu> menus, int origin, int index)
        {
            var sort = 1;
            if (!menus.Any())
                return sort;
            if (origin == index)
                return index;

            var maxSort = menus.Max(m => m.Sort);
            if (index > maxSort)
            {
                sort = maxSort + 1;
                menus.FindAll(m => m.Sort > origin && m.Sort <= maxSort).ForEach(s =>
                {
                    s.DecreaseSort();
                });
            }
            else
            {
                sort = index;
                menus.FindAll(m => m.Sort > origin && m.Sort <= index).ForEach(s =>
                {
                    s.DecreaseSort();
                });
            }

            return sort;
        }

        private List<Menu> GetMenus(IMenuRepository menuRepository, ISpecification<Menu> spec, Guid menuId, bool isExistMenu)
        {
            var specification = spec;
            if (isExistMenu)
                specification = spec.AndNotSpecification(Specification<Menu>.Eval(m => m.ID == menuId));
            return menuRepository.FindAll(specification).ToList();
        }

        #endregion
    }
}
