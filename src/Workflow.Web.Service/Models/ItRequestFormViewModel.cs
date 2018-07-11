/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Web.Models.ItRequestForm;

namespace Workflow.Web.Models
{
    public class ItRequestFormViewModel: AbstractFormDataViewModel
    {

        private DataItem _dataItem;

        public DataItem dataItem
        {
            get {
                return _dataItem ?? (_dataItem = new DataItem());
            }
            set { _dataItem = value; }
        }
    }

    public class DataItem
    {
        public IEnumerable<RequestItemViewModel> requestItems { get; set; }
        public IEnumerable<RequestItemViewModel> delRequestItems { get; set; }
        public IEnumerable<RequestItemViewModel> editRequestItems { get; set; }
        public IEnumerable<RequestItemViewModel> addRequestItems { get; set; }


        public IEnumerable<RequestUserViewModel> requestUsers { get; set; }
        public IEnumerable<RequestUserViewModel> delRequestUsers { get; set; }
        public IEnumerable<RequestUserViewModel> editRequestUsers { get; set; }
        public IEnumerable<RequestUserViewModel> addRequestUsers { get; set; }
    }
}
