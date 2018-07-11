using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.HumanResource;
using Workflow.Domain.Entities.Attachment;

namespace Workflow.DataAcess.Repositories.Attachment
{
    public class RequisitionRepository : RepositoryBase<Requisition>, IRequisitionRepository
    {
        public RequisitionRepository(IDbFactory dbFactory) : base(dbFactory) { }
        
    }
}
