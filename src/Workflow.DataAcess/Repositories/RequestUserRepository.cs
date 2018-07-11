/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories
{
    public class RequestUserRepository: RepositoryBase<RequestUser>, IRequestUserRepository
    {
        public RequestUserRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public virtual IEnumerable<RequestUser> GetRequestUserByRequestHeaderId(int requestHeaderId)
        {

            IDbSet<RequestUser> dbSet = DbContext.Set<RequestUser>();
            return dbSet.Where(p=>p.RequestHeaderId==requestHeaderId).ToList();

        }
    }
}
