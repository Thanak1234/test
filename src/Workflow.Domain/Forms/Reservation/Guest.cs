using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Reservation
{
    [DataContract]
    public class Guest
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "companyName")]
        public string CompanyName { get; set; }


        [DataMember(Name = "requestHeaderId")]
        public int RequestHeaderId { get; set; }            
       
    }
}
