using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.MWO;
using Workflow.Domain.Entities.Core;
using Workflow.Domain.Entities.MWO;

namespace Workflow.Web.Models.AvdrForm {
    public class AvdrFormViewModel : AbstractFormDataViewModel {
        private DataItem _dataItem;

        public DataItem dataItem {
            get {
                return _dataItem ?? (_dataItem = new DataItem());
            }
            set { _dataItem = value; }
        }

        public class DataItem {
            public Avdr FormRequestData { get; set; }
        }
    }
}
