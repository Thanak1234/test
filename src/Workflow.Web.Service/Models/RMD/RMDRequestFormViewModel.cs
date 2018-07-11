using System.Collections.Generic;
using Workflow.Web.Models.RMD;

namespace Workflow.Web.Models.RMD
{
    public class RMDRequestFormViewModel : AbstractFormDataViewModel
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
        public RiskAssessmentViewModel riskAssessment { get; set; }

        public IEnumerable<Worksheet1ViewModel> worksheet1s { get; set; }
        public IEnumerable<Worksheet1ViewModel> addWorksheet1s { get; set; }
        public IEnumerable<Worksheet1ViewModel> editWorksheet1s { get; set; }
        public IEnumerable<Worksheet1ViewModel> delWorksheet1s { get; set; }

        public IEnumerable<Worksheet2ViewModel> worksheet2s { get; set; }
        public IEnumerable<Worksheet2ViewModel> addWorksheet2s { get; set; }
        public IEnumerable<Worksheet2ViewModel> editWorksheet2s { get; set; }
        public IEnumerable<Worksheet2ViewModel> delWorksheet2s { get; set; }

        public IEnumerable<Worksheet3ViewModel> worksheet3s { get; set; }
        public IEnumerable<Worksheet3ViewModel> addWorksheet3s { get; set; }
        public IEnumerable<Worksheet3ViewModel> editWorksheet3s { get; set; }
        public IEnumerable<Worksheet3ViewModel> delWorksheet3s { get; set; }
    }
}
