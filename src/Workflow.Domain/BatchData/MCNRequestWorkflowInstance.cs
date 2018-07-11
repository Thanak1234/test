using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.EGM;

namespace Workflow.Domain.Entities.BatchData.EGMInstance
{
    public class MCNRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public IEnumerable<RequestUserExt> MachineEmployeeList { get; set; }
        public IEnumerable<RequestUserExt> AddMachineEmployeeList { get; set; }
        public IEnumerable<RequestUserExt> EditMachineEmployeeList { get; set; }
        public IEnumerable<RequestUserExt> DelMachineEmployeeList { get; set; }

        public Machine Machine { get; set; }
    }
}
