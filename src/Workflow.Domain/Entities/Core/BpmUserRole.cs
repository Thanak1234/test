using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Core {
    public class BpmUserRole: BaseEntity
    {
        public string RoleCode { get; set; }
        public int EmpId { get; set; }
        public string Status { get; set; }
    }
}
