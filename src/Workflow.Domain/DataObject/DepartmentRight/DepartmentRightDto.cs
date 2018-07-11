using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.DepartmentRight
{
    [DataContract]
    public class DepartmentRightDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "deptcode")]
        public string DeptCode { get; set; }

        [DataMember(Name = "deptname")]
        public string Deptname { get; set; }

        [DataMember(Name = "depttype")]
        public string DeptType { get; set; }      
    }
}
