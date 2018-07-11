/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Core;
using Workflow.Domain.Entities.IT;

namespace Workflow.Domain.Entities.BatchData
{
    public class AvirFormWorkflowInstance : AbstractWorkflowInstance
    {
        public Avir FormRequestData { get; set; }
    }
}
