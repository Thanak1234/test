using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class FormIntegratedDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "formNo")]
        public string FormNo { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "pNo")]
        public int PNo { get; set; }

        [DataMember(Name = "formIntegrated")]
        public bool FormIntegrated { get; set; }
    }
}
