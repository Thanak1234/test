
using System.Collections.Generic;
using Workflow.Domain.Entities.Forms;
/**
*@author : Yim Samaune
*/

namespace Workflow.Domain.Entities.BatchData
{
    public class JRAMRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public RamClear RamClear { get; set; }
    }
}