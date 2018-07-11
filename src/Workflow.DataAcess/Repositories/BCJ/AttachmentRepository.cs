using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.DataAcess.Repositories.BCJ
{
    public class AttachmentRepository : RepositoryBase<BcjAttachment>, IAttachmentRepository
    {
        public AttachmentRepository(IDbFactory dbFactory) : base(dbFactory) { }
        
    }
}
