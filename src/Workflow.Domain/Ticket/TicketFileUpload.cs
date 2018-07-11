using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketFileUpload : AbstractBaseEntity
    {
        public int ActivityId { get; set; }
        public string FileName { get; set; }
        public string Ext { get; set; }
        public string UploadSerial { get; set; }
    }
}
