using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.EGM;

namespace Workflow.Web.Models.AttandanceRequestForm
{
    public class ATDFormViewModel : AbstractFormDataViewModel
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
        public Attandance Attandance { get; set; }
    }
}
