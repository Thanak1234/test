/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities
{
    public class Department 
    {

        public int Id { get; set; }
        public int MainId { get; set; }
        public string DeptCode { get; set; }
        public string Deptname { get; set; }
        public string DeptType { get; set; }
        public int? HoD { get; set; }
        public int Status { get; set; }


    }

    public class DeptLookup
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}