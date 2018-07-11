using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.MWO;
using Workflow.Domain.Entities.INCIDENT;
using Workflow.Domain.Entities.MWO;
using Workflow.Web.Models.ItRequestForm;

namespace Workflow.Web.Models.IncidentRequestForm
{
    public class ICDFormViewModel : AbstractFormDataViewModel
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
        public IEnumerable<RequestUserViewModel> IncidentEmployeeList { get; set; }
        public IEnumerable<RequestUserViewModel> AddIncidentEmployee { get; set; }
        public IEnumerable<RequestUserViewModel> DelIncidentEmployee { get; set; }
        public IEnumerable<RequestUserViewModel> EditIncidentEmployee { get; set; }

        public Incident Incident { get; set; }
    }
}
