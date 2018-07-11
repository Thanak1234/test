using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.Email;

namespace Workflow.DataAcess.Repositories.Email
{
    public class EmailItemRepository : RepositoryBase<EmailItem>, IEmailItemRepository
    {
        public EmailItemRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public List<MailAttachments> getMailAttach(EmailItem item)
        {
            return DbContext.Set<MailAttachments>().Where(t=>t.MailItemId == item.Id).ToList();
        }
    }
}
