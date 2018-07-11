using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.K2
{
    public class RequestApplication
    {
        public int Id { get; set; }
        public string RequestCode { get; set; }
        public string RequestName { get; set; }
        public string ProcessName { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessPath { get; set; }
        public string FormXType { get; set; }
        public int GenNumber { get; set; }
        public bool Active { get; set; }
    }
}
