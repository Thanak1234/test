using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.INCIDENT;

namespace Workflow.Domain.Entities.BatchData.IncidentInstance
{
    public class ICDRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public IEnumerable<RequestUserExt> IncidentEmployeeList { get; set; }
        public IEnumerable<RequestUserExt> AddIncidentEmployee { get; set; }
        public IEnumerable<RequestUserExt> DelIncidentEmployee { get; set; }
        public IEnumerable<RequestUserExt> EditIncidentEmployee { get; set; }

        public Incident Incident { get; set; }
    }
}
