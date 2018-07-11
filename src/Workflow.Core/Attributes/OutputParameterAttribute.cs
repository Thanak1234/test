using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Core.Attributes
{
    public class OutputParameterAttribute: Attribute
    {
        public Type ReturnType { get; set; }
    }
}
