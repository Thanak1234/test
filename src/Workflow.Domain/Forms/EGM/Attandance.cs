﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Workflow.Domain.Entities.EGM
{
    [DataContract]
    public class Attandance
    {
        [DataMember(Name = "id")]
        public int id { get; set; }

        [DataMember(Name = "detail")]
        public string detail { get; set; }

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
