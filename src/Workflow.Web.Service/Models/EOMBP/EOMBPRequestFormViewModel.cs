using System.Collections.Generic;

namespace Workflow.Web.Models.EOMBP
{
    public class EOMBPRequestFormViewModel : AbstractFormDataViewModel
    {
        private DataItem _dataItem;

        public DataItem dataItem
        {
            get
            {
                return _dataItem ?? (_dataItem = new DataItem());
            }
            set { _dataItem = value; }
        }
    }

    public class DataItem
    {
        public BestPerformanceViewModel bestPerformance { get; set; }
        public IEnumerable<BestPerformanceDetailViewModel> bestPerformanceDetails { get; set; }
        public IEnumerable<BestPerformanceDetailViewModel> addBestPerformanceDetails { get; set; }
        public IEnumerable<BestPerformanceDetailViewModel> editBestPerformanceDetails { get; set; }
        public IEnumerable<BestPerformanceDetailViewModel> delBestPerformanceDetails { get; set; }

        public IEnumerable<EmployeeOfMonthDetailViewModel> employeeOfMonthDetails { get; set; }
        public IEnumerable<EmployeeOfMonthDetailViewModel> addEmployeeOfMonthDetails { get; set; }
        public IEnumerable<EmployeeOfMonthDetailViewModel> editEmployeeOfMonthDetails { get; set; }
        public IEnumerable<EmployeeOfMonthDetailViewModel> delEmployeeOfMonthDetails { get; set; }
    }
}
