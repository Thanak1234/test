using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Repositories.Reservation
{
    public class AttachmentRepository : RepositoryBase<FnFAttachment>, IAttachmentRepository
    {
        public AttachmentRepository(IDbFactory dbFactory) : base(dbFactory) { }
        
    }
}
