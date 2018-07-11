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
    public class ComplimentaryRepository : RepositoryBase<Complimentary>, IComplimentaryRepository
    {
        public ComplimentaryRepository(IDbFactory dbFactory) : base(dbFactory) { }
        
    }
}
