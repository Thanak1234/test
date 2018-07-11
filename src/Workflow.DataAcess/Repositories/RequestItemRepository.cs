/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.IT;

namespace Workflow.DataAcess.Repositories
{
    public class RequestItemRepository : RepositoryBase<RequestItem> , IRequestItemRepository
    {
        public RequestItemRepository(IDbFactory dbFactory): base(dbFactory) { }
    }
}
