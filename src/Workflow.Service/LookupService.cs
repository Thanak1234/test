/**
*@author : Phanny
*/

using System.Collections.Generic;
using System.Linq;
using Workflow.DataAcess;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.Domain.Entities;
using Workflow.Framework;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class LookupService : ILookupService
    {
        IMenuRepository menuReposity = null;
        ILookupRepository lookupReposity = null;
        ILookupRepository lookupChildReposity = null;
        
        
        public LookupService ()
        {
            menuReposity = new MenuRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            lookupReposity = new LookupRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            lookupChildReposity = new LookupRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
        }

        public IEnumerable<MenuDto> getMenus(string userName)
        {
            var menuList =  menuReposity.GetMenusByLoginName(userName);

            var startedWorklist = new ProcInstProvider(userName).StartWorkflowList();
            
            var allMenus = menuList.ToList();
            var menus = GetAllMenus(0, allMenus, startedWorklist);

            menus = GetMenusWithChildrens(menus);

            return menus;
        }

        private List<MenuDto> GetMenusWithChildrens(List<MenuDto> nodes) {
            return nodes.Where(p => p.NoChild || (p.children != null && p.children.Count() > 0))
                .Select(x => new MenuDto() {
                    text = x.text,
                    desc = x.desc,
                    url = x.url,
                    iconCls = x.iconCls,
                    routeId = x.routeId,
                    view = x.view,
                    closableTab = x.closableTab,
                    NoChild = x.NoChild,
                    leaf = x.NoChild,
                    children = GetMenusWithChildrens(x.children)
                }).ToList();
        }

        public List<MenuDto> GetAllMenus(int parentid, List<Menu> menus, IEnumerable<string> startedWorklist) {
            return menus.Where(p => (p.ParentId == parentid && p.Active ) && (p.IsWorkflow ? startedWorklist.Contains(p.Workflow) : true))
                    .OrderBy(o => o.Sequence)
                    .Select(men => new MenuDto() {
                        text = men.MenuName,
                        desc = men.MenuDesc,
                        url = men.MenuUrl,
                        iconCls = men.IconCls,
                        routeId = men.RouteId,
                        view = men.ViewClass,
                        closableTab = men.ClosableTab,
                        NoChild = men.NoChild,
                        leaf = men.NoChild,
                        children = GetAllMenus(men.Id, menus, startedWorklist)
                    })
                    .ToList();
        }

        private MenuDto convert(Menu menu)
        {
            var item = new MenuDto()
            {
                text        = menu.MenuName,
                desc        = menu.MenuDesc,
                url         = menu.MenuUrl,
                iconCls     = menu.IconCls,
                routeId     = menu.RouteId,
                view        = menu.ViewClass,
                closableTab = menu.ClosableTab,
                NoChild     = menu.NoChild,
                leaf = true
            };

            return item;
        }

        /* LOOK UP - END */
        public IQueryable<T> GetLookups<T>() where T : class
        {
            var lookups = lookupReposity.GetDataSet<T>();
            return (from p in lookups select p);

        }

        public IQueryable<Lookup> LookupByName(string name, int parentId)
        {
            IQueryable<Lookup> lookups;
            if (parentId == -1)
            { // return children only
                lookups = lookupReposity.Lookups().Where(p => p.Name == name && p.ParentId > 0).OrderBy(o=>o.Sequence);
                lookups.Each(p =>
                {
                    p.HasChild = HasChild(p.Id);
                });
            } else {
                lookups = lookupReposity.Lookups().Where(p => p.Name == name && p.ParentId == parentId).OrderBy(o=>o.Sequence);
                lookups.Each(p => {
                    p.HasChild = HasChild(p.Id);
                });
            }

            return lookups;
        }

        public IQueryable<Lookup> LookupByName(string name, int parentId, int id)
        {
            IQueryable<Lookup> lookups;
            if (parentId == -1)
            { // return children only
                lookups = lookupReposity.Lookups().Where(p => p.Name == name && p.ParentId > 0).OrderBy(o => o.Sequence);
                lookups.Each(p =>
                {
                    p.HasChild = HasChild(p.Id);
                });
            }
            else if (parentId > 0)
            {
                lookups = lookupReposity.Lookups().Where(p => p.Name == name && p.ParentId == parentId).OrderBy(o => o.Sequence);
                lookups.Each(p =>
                {
                    p.HasChild = HasChild(p.Id);
                });
            }
            else if (parentId == 0 && id > 0)
            {
                lookups = lookupReposity.Lookups().Where(p => p.Name == name && p.Id == id).OrderBy(o => o.Sequence);
            }
            else lookups = null;

            return lookups;
        }

        private bool HasChild(int id) {
            return lookupChildReposity.Lookups().FirstOrDefault(p => p.ParentId == id) != null;
        }
    }
}
