/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories
{
    public class MenuRepository : RepositoryBase<Menu> , IMenuRepository
    {
        public MenuRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<Menu> GetMenusByLoginName(string loginName) {
            return DbContext.Database.SqlQuery<Menu>("EXECUTE [ADMIN].[SP_MENU] @USER=@USER", new object[] {
                    new SqlParameter("@USER", loginName)
                });
        }
    }
}
