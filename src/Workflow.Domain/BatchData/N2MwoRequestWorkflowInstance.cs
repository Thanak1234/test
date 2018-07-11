using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.MWO;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.Domain.Entities.BatchData
{
    public class N2MwoRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public N2MWOInformation Information { get; set; }

    }
}
