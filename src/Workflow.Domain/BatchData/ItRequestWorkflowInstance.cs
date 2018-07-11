/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.IT;

namespace Workflow.Domain.Entities.BatchData
{
    public class ItRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public IEnumerable<RequestUser> RequestUsers { get; set; }
        public IEnumerable<RequestUser> DelRequestUsers { get; set; }
        public IEnumerable<RequestUser> EditRequestUsers { get; set; }
        public IEnumerable<RequestUser> AddRequestUsers { get; set; }

        public IEnumerable<RequestItem> RequestItems { get; set; }
        public IEnumerable<RequestItem> DelRequestItems { get; set; }
        public IEnumerable<RequestItem> EditRequestItems { get; set; }
        public IEnumerable<RequestItem> AddRequestItems { get; set; }
    }
}
