using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.EGM;
namespace Workflow.Domain.Entities.BatchData.EGMInstance
{
    public class ATDRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public Attandance Attandance { get; set; }
    }
}
