using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Attachment;

namespace Workflow.DataAcess.Repositories
{
    public class AttachmentRepository : RepositoryBase<Document>, IAttachmentRepository
    {
        public AttachmentRepository(IDbFactory dbFactory) : base(dbFactory) { }
        
    }
}
