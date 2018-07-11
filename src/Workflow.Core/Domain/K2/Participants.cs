using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Workflow.DataContract.K2
{

    [Serializable()]
    [XmlRoot("Participants")]
    public class Participants {
        [XmlElement]
        public List<Participant> Participant { get; set; }
    }

    public class Participant {
        [XmlAttribute]
        public string User { get; set; }

        [XmlAttribute]
        public string StartDate { get; set; }

        [XmlAttribute]
        public string FinishDate { get; set; }

        [XmlAttribute]
        public string Status { get; set; }

        [XmlAttribute]
        public string Action { get; set; }
    }
}
