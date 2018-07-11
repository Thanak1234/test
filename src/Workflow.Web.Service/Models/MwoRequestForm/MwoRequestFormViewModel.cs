using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.MWO;
using Workflow.Domain.Entities.MWO;

namespace Workflow.Web.Models.MwoRequestForm {
    public class MwoRequestFormViewModel: AbstractFormDataViewModel {
        private DataItem _dataItem;

        public DataItem dataItem {
            get {
                return _dataItem ?? (_dataItem = new DataItem());
            }
            set { _dataItem = value; }
        }        
    }

    public class DataItem {

        public RequestInformation requestInformation { get; set; }
        public MaintenanceDepartment maintenanceDepartment { get; set; }

        public IEnumerable<ItemView> storeItems { get; set; }

        public IEnumerable<ItemView> technicianItems { get; set; }
    }
}
