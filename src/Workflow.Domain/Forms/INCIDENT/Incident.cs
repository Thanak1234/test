using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.INCIDENT
{
    [DataContract]
    public class Incident
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        
        [DataMember(Name = "mcid")]
        public string mcid { get; set; }
        
	    [DataMember(Name = "gamename")]
        public string gamename { get; set;}
        
	    [DataMember(Name = "zone")]
        public string zone { get; set; }

        [DataMember(Name = "customername")]
        public string customername { get; set; }

        [DataMember(Name = "cctv")]
        public string cctv { get; set; }
        
	    [DataMember(Name = "subject")]
        public string subject { get; set; }

	    [DataMember(Name = "outline")]
        public string  outline { get; set; }

	    [DataMember(Name = "remarks")]
        public string remarks { get; set; }

	    [DataMember(Name = "created_date")]
        public DateTime created_date { get; set; }

        [DataMember(Name = "created_by")]
        public string created_by { get; set; }

        [DataMember(Name = "modified_date")]
        public DateTime modified_date { get; set; }

        [DataMember(Name = "modified_by")]
        public string modified_by { get; set; }

	    [DataMember(Name = "requestheaderid")]
        public int requestheaderid { get; set; }
        
    }
}
