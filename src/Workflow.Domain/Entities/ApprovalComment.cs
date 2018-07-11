/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities
{
    public class ApprovalComment: AbstractBaseEntity
    {

        public int RequestHeaderId { get; set; }
        public RequestHeader RequestHeader { get; set; }
        public int ActInstId { get; set; }
        public string Activity { get; set; }
        public string ApplicationName { get; set; }
        public string Approver { get; set; }
        public string ApproverDisplayName { get; set; }
        public string Decision { get; set; }
        public string Comments { get; set; }
        public Nullable<int> Version { get; set; }
    }
}
