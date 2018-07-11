using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities
{
    public class RequestUser
    {
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public virtual RequestHeader RequestHeader { get; set; }

        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpNo { get; set; }
        public int TeamId { get; set; }
        public virtual Department Team { get; set; }

        public string Position { get; set; }
        public string Email { get; set; }
        public DateTime? HiredDate { get; set; }
        public string Manager { get; set; }
        public string Phone { get; set; }
        public Nullable<int> Version { get; set; }
    }   
}