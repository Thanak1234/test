using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataContract.K2
{
    public class OOFCriteria
    {
        public OOFCriteria() {
            DestUsers = new List<string>();
            Exceptions = new List<OOFException>();
        }
        public List<string> DestUsers { get; set; }
        public List<OOFException> Exceptions { get; set; }
        public int ShareType { get; set; }
    }

    public class OOFException {
        public string Name { get; set; }
        public string ProcessFullPath { get; set; }
        public string ActivityName { get; set; }
    }
}
