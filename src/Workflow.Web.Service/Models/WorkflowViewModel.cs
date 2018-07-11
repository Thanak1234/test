/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Core;

namespace Workflow.Web.Models
{
    [DataContract]
    public class WorkflowViewModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "requestCode")]
        public string RequestCode { get; set; }
        
        [DataMember(Name = "processName")]
        public string ProcessName { get; set; }

        [DataMember(Name = "processCode")]
        public string ProcessCode { get; set; }

        [DataMember(Name = "processPath")]
        public string ProcessPath { get; set; }

        [DataMember(Name = "formNumber")]
        public int GenId { get; set; }

        [DataMember(Name = "xtype")]
        public string FormXType { get; set; }

        [DataMember(Name = "active")]
        public bool? Active { get; set; }
        
        [DataMember(Name = "user")]
        public IList<Users> Users { get; set; }
    }

    [DataContract]
    public class Users {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "loginName")]
        public string LoginName { get; set; }
    }
}
