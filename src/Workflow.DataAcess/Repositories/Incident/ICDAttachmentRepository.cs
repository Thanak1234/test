using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.INCIDENT;
namespace Workflow.DataAcess.Repositories.Incident
{
    public class ICDAttachmentRepository : RepositoryBase<IncidentAttachement>,IICDAttachmentRepository
    {
        public ICDAttachmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
