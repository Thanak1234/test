/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.Domain.Entities;

namespace Workflow.Service.Interfaces
{
    public interface ILookupService 
    {
        IEnumerable<MenuDto> getMenus(string userName);

        IQueryable<T> GetLookups<T>() where T : class;
        IQueryable<Lookup> LookupByName(string name, int parentId = 0); // {-1}:children only 
        //IQueryable<Lookup> LookupChildren(int parentId);
        IQueryable<Lookup> LookupByName(string name, int parentId = 0, int id = 0); 
    }
}
