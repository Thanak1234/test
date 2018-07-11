/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;

namespace Workflow.Web.Models
{
    public abstract class AbstractFormDataViewModel
    {

        public int requestHeaderId { get; set; }
        public string requestNo { get; set; }
        public string action { get; set; }
        public IList<string> actions { get; set; }
        public string comment { get; set; }
        public string activity { get; set; }
        public string state { get; set; }
        public string lastActivity { get; set; }
        public string status { get; set; }
        public string serial { get; set; }
        public short priority { get; set; }
        public string FileUploadSerial { get; set; }

        public RequestorViewModel requestor { get; set; }
        public FileUploadItemsViewModel fileUploads { get; set; }
        public IEnumerable<ActivityHistoryViewModel> activities { get; set; }

        /* 
         * on/off panel section/control base on activity and user/role 
         */
        public object property { get; set; }
        public object acl { get; set; }
    }
}
