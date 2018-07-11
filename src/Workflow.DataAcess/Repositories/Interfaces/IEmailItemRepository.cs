using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Email;

namespace Workflow.DataAcess.Repositories.Interfaces
{
    public interface IEmailItemRepository : IRepository<EmailItem>
    {
        List<MailAttachments> getMailAttach(EmailItem item);
    }
}
