/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models
{
    public class ActivityHistoryViewModel
    {

        public string activity { get; set; }

        public DateTime actionDate { get; set; }

        public string approver { get; set; }

        public string appriverDisplayName { get; set; }


        public string decision { get; set; }

        public string comment { get; set; }

    }
}
