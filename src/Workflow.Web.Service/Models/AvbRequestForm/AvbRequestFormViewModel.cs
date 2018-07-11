/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models.AvbRequestForm
{
    public class AvbRequestFormViewModel : AbstractFormDataViewModel
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

        public AvbJobDetailViewModel jobDetail { get; set; }
        public IEnumerable<RequestItemViewModel> items { get; set; }
        public IEnumerable<RequestItemViewModel> addItems { get; set; }
        public IEnumerable<RequestItemViewModel> editItems { get; set; }
        public IEnumerable<RequestItemViewModel> delItems { get; set; }
    }
}
