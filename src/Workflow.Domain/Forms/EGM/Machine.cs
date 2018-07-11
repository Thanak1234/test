using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Workflow.Domain.Entities.EGM
{
    [DataContract]
    public class Machine
    {
        [DataMember(Name = "id")]
        public int id { get; set; }

        [DataMember(Name = "mcid")]
        public string mcid { get; set; }

	    [DataMember(Name = "gamename")]
        public string gamename { get; set; }
        
	    [DataMember(Name = "zone")]
        public string zone { get; set; }
        	    
	    [DataMember(Name = "type")]
        public string type { get; set; }
        
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
        
	    [DataMember(Name = "request_header_id")]
        public int request_header_id { get; set; }
        
    }
}
