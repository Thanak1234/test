/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models.HumanResource.ATT
{
    public class ATTFlightDetailViewModel
    {
        public int id { get; set; }
        public string fromDestination { get; set; }
        public string toDestination { get; set; }
        public DateTime date { get; set; }
        public DateTime time { get; set; }
        public int requestHeaderId { get; set; }
    }
}
