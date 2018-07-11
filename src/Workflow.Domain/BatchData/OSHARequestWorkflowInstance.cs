using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.MWO;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.MWO;
using Workflow.Domain.Entities.OSHA;

namespace Workflow.Domain.Entities.BatchData
{
    public class OSHARequestWorkflowInstance : AbstractWorkflowInstance
    {
        public OSHAInformation Information { get; set; }

        public List<OSHAEmployee> Victims { get; set; }
        public List<OSHAEmployee> AddVictims { get; set; }
        public List<OSHAEmployee> EditVictims { get; set; }
        public List<OSHAEmployee> RemoveVictims { get; set; }

        public List<OSHAEmployee> Withness { get; set; }
        public List<OSHAEmployee> AddWithness { get; set; }
        public List<OSHAEmployee> EditWithness { get; set; }
        public List<OSHAEmployee> RemoveWithness { get; set; }
    }
}
