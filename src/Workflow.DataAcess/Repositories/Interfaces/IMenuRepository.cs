/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories.Interfaces
{
    public interface IMenuRepository : IRepository<Menu>
    {
        IEnumerable<Menu> GetMenusByLoginName(string loginName);
    }
}
