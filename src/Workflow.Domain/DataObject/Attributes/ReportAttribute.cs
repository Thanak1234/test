using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class |AttributeTargets.Struct)]
    public class ReportAttribute : Attribute
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public ReportAttribute()
        {
            
        }
    }
}
