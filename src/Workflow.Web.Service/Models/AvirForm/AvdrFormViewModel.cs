using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.MWO;
using Workflow.Domain.Entities.Core;
using Workflow.Domain.Entities.MWO;

namespace Workflow.Web.Models.AvirForm {
    public class AvirFormViewModel : AbstractFormDataViewModel {
        private DataItem _dataItem;

        public DataItem dataItem {
            get {
                return _dataItem ?? (_dataItem = new DataItem());
            }
            set { _dataItem = value; }
        }        
    }
    public class DataItem {
        public Avir FormRequestData { get; set; }
    }
}
