
using System.Collections.Generic;
using Workflow.Domain.Entities.RMD;
/**
*@author : Yim Samaune
*/

namespace Workflow.Domain.Entities.BatchData
{
    public class RMDRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public RiskAssessment RiskAssessment { get; set; }

        public IEnumerable<Worksheet1> Worksheet1s { get; set; }
        public IEnumerable<Worksheet1> DelWorksheet1s { get; set; }
        public IEnumerable<Worksheet1> EditWorksheet1s { get; set; }
        public IEnumerable<Worksheet1> AddWorksheet1s { get; set; }

        public IEnumerable<Worksheet2> Worksheet2s { get; set; }
        public IEnumerable<Worksheet2> DelWorksheet2s { get; set; }
        public IEnumerable<Worksheet2> EditWorksheet2s { get; set; }
        public IEnumerable<Worksheet2> AddWorksheet2s { get; set; }

        public IEnumerable<Worksheet3> Worksheet3s { get; set; }
        public IEnumerable<Worksheet3> DelWorksheet3s { get; set; }
        public IEnumerable<Worksheet3> EditWorksheet3s { get; set; }
        public IEnumerable<Worksheet3> AddWorksheet3s { get; set; }

    }
}