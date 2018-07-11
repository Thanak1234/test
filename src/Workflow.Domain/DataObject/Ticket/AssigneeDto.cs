using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class AssigneeDto
    {
        public string VirtualId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsTeam { get; set; }
    }
}
