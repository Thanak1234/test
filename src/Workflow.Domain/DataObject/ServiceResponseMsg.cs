using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject
{
    public class ServiceResponseMsg
    {
        //0 = failed, 1=success, 2=existed

        public int status { get; set; }
        public string message { get; set; }
        public object obj { get; set; }
    }
}
