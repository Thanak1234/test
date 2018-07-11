/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Entities = Workflow.Domain.Entities.EOMRequestForm;

namespace Workflow.Web.Models.EOM
{
    public class EOMRequestFormViewModel : AbstractFormDataViewModel
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

    [DataContract]
    public class DataItem
    {
        [DataMember(Name = "eomInfo")]
        public Domain.Entities.EOMRequestForm.EOMDetail EOMInfo { get; set; }
    }
}
