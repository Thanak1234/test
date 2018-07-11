
using System.Collections.Generic;
using Workflow.Domain.Entities.Forms;
/**
*@author : Yim Samaune
*/

namespace Workflow.Domain.Entities.BatchData
{
    public class GMURequestWorkflowInstance : AbstractWorkflowInstance
    {
        public GmuRamClear GmuRamClear { get; set; }
    }
}