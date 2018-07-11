using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Workflow.Web.Service.Models
{
    [DataContract]
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }
}