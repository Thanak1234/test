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
    public class AvbJobDetailViewModel
    {
        public int id { get; set; }
        public string projectName { get; set; }
        public string location { get; set; }
        public DateTime setupDate { get; set; }
        public DateTime actualDate { get; set; }
        public string projectBrief { get; set; }
        public string other { get; set; }
    }
}
