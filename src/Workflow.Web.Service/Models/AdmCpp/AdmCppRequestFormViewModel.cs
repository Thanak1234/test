using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.ADMCPPForm;

namespace Workflow.Web.Models.AdmCpp
{
    public class AdmCppRequestFormViewModel : AbstractFormDataViewModel
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
        [DataMember(Name = "admcpp")]
        public ADMCPP AdmCpp { get; set; }
    }
}
