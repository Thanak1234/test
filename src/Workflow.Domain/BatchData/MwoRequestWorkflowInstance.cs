using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.MWO;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.MWO;

namespace Workflow.Domain.Entities.BatchData
{
    public class MwoRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public MWOInformation Information { get; set; }

    }
}
