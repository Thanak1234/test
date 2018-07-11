using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Workflow.App.Service.Models
{
    [XmlRoot("DestinationUser")]
    public class DestinationUser
    {
        [XmlElement("ACCOUNT")]
        public string Account { get; set; }
        [XmlElement("EMAIL")]
        public string Email { get; set; }
    }
    
}