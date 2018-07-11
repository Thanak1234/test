/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models.ItRequestForm
{
    public class RequestUserViewModel
    {
        public int id { get; set; }
        public string empId { get; set; }
        public string empName { get; set; }
        public string empNo { get; set; }
        public int teamId { get; set; }
        public string teamName { get; set; }
        public string position { get; set; }
        public string email { get; set; }
        public DateTime? hiredDate { get; set; }
        public string manager { get; set; }
        public string phone { get; set; }
    }
}
