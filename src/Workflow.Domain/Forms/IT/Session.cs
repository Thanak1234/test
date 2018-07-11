
/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.IT
{
    public class Session
    {
        public int Id { get; set; }
        public string SessionName { get; set; }
        public string Description { get; set; }
    }
}
