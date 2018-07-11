using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;

namespace Workflow.Business.Ticketing.Dto
{
    public class AbstractTicketParam
    {
        public int TicketId { get; set; }
        public string ActivityCode { get; set; }
        public string ActionCode { get; set; }
        public int CurrLoginUserId { get; set; }
        public string ActComment { get; set; }
        public List<FileUploadInfo> FileUploads { get; set; }
        public bool bySystem { get; set; } = false;
    }
}
