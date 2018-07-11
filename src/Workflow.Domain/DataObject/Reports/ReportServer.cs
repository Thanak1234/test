using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Reports
{
    public class ReportServer
    {
        public Guid Id { get; set; } 

        public string Name { get; set; }

        public string CommandType { get; set; }

        public string ParameterText { get; set; }

        public string CommandText { get; set; }
    }
}
