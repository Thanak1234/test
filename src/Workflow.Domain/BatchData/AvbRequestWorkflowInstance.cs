/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.AV;

namespace Workflow.Domain.Entities.BatchData
{
    public class AvbRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public IEnumerable<AvbRequestItem> AvbRequestItems { get; set; }
        public IEnumerable<AvbRequestItem> DelAvbRequestItems { get; set; }
        public IEnumerable<AvbRequestItem> EditAvbRequestItems { get; set; }
        public IEnumerable<AvbRequestItem> AddAvbRequestItems { get; set; }
        public AvbJobHistory AvbJobHistory { get; set; }
    }
}
