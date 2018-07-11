using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Attachment;


namespace Workflow.DataAcess.Repositories.Attachment
{
    public class TravelDetailRepository : RepositoryBase<TravelDetail>, ITravelDetailRepository
    {
        public TravelDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }
        
    }
}
