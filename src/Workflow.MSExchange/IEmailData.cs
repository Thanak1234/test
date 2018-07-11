using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.MSExchange.Core;

namespace Workflow.MSExchange
{
    public interface IEmailData
    {
        List<string> Recipients { get; set; }
        List<string> Ccs { get; set; }
        List<string> Bccs { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        List<EmailFileAttachment> AttachmentFiles { get; set; }
        object Model { get; set; }
    }
}
