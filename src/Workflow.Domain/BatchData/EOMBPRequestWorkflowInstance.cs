/**
*@author : Yim Samaune
*/
using System.Collections.Generic;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Domain.Entities.BatchData
{
    public class EOMBPRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public BestPerformance BestPerformance { get; set; }

        public IEnumerable<BestPerformanceDetail> BestPerformanceDetails { get; set; }
        public IEnumerable<BestPerformanceDetail> DelBestPerformanceDetails { get; set; }
        public IEnumerable<BestPerformanceDetail> EditBestPerformanceDetails { get; set; }
        public IEnumerable<BestPerformanceDetail> AddBestPerformanceDetails { get; set; }

        public IEnumerable<BestPerformanceDetail> EmployeeOfMonthDetails { get; set; }
        public IEnumerable<BestPerformanceDetail> DelEmployeeOfMonthDetails { get; set; }
        public IEnumerable<BestPerformanceDetail> EditEmployeeOfMonthDetails { get; set; }
        public IEnumerable<BestPerformanceDetail> AddEmployeeOfMonthDetails { get; set; }
    }
}