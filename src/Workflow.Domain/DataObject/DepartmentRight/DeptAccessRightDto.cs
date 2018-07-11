using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.DepartmentRight
{
    [DataContract]
    public class DeptAccessRightDto
    {
        [DataMember(Name = "id")]
        public int ID { get; set; } 
        [DataMember(Name = "userid")]
        public int USER_ID { get; set; }
        [DataMember(Name = "deptid")]
        public int DEPT_ID { get; set; }
        [DataMember(Name = "reqapp")]
        public string REQ_APP { get; set; }        
        [DataMember(Name = "requestdesc")]
        public string REQUEST_DESC { get; set; }
        [DataMember(Name = "displayname")]
        public string DISPLAY_NAME { get; set; }
        [DataMember(Name = "deptname")]
        public string DEPT_NAME { get; set; }
        [DataMember(Name = "empno")]
        public string EMP_NO { get; set; }
        [DataMember(Name = "createdby")]
        public string CREATED_BY { get; set; }
        [DataMember(Name = "createddate")]
        public DateTime? CREATED_DATE { get; set; }
        [DataMember(Name = "modifiedby")]
        public string MODIFIED_BY { get; set; }
        [DataMember(Name = "modifieddate")]
        public DateTime? MODIFIED_DATE { get; set; }
        [DataMember(Name = "status")]
        public string STATUS { get; set; }
    }
}
