/**
*@author : Yim Samaune
*/
using System.Collections.Generic;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Domain.Entities.BatchData
{
    public class ATCFRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public IEnumerable<AdditionalTimeWorked> AdditionalTimeWorkeds { get; set; }
        public IEnumerable<AdditionalTimeWorked> DelAdditionalTimeWorkeds { get; set; }
        public IEnumerable<AdditionalTimeWorked> EditAdditionalTimeWorkeds { get; set; }
        public IEnumerable<AdditionalTimeWorked> AddAdditionalTimeWorkeds { get; set; }
    }
}