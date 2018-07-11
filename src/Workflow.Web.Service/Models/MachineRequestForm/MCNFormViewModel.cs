using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.EGM;
using Workflow.Web.Models.ItRequestForm;

namespace Workflow.Web.Models.MachineRequestForm
{
    public class MCNFormViewModel : AbstractFormDataViewModel
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
        public IEnumerable<RequestUserViewModel> MachineEmployeeList { get; set; }
        public IEnumerable<RequestUserViewModel> AddMachineEmployee { get; set; }
        public IEnumerable<RequestUserViewModel> DelMachineEmployee { get; set; }
        public IEnumerable<RequestUserViewModel> EditMachineEmployee { get; set; }

        public Machine Machine { get; set; }
    }
}
