using System.Collections.Generic;

namespace Workflow.Web.Models.MTFRequestForm
{
    public class MTFRequestFormViewModel : AbstractFormDataViewModel
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
        public TreatmentViewModel treatment { get; set; }
        public IEnumerable<PrescriptionViewModel> prescriptions { get; set; }
        public IEnumerable<PrescriptionViewModel> addPrescriptions { get; set; }
        public IEnumerable<PrescriptionViewModel> editPrescriptions { get; set; }
        public IEnumerable<PrescriptionViewModel> delPrescriptions { get; set; }

        public IEnumerable<UnfitToWorkViewModel> unfitToWorks { get; set; }
        public IEnumerable<UnfitToWorkViewModel> addUnfitToWorks { get; set; }
        public IEnumerable<UnfitToWorkViewModel> editUnfitToWorks { get; set; }
        public IEnumerable<UnfitToWorkViewModel> delUnfitToWorks { get; set; }
    }
}
