﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstATTParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }

        [DataMember(Name = "DetailType")]
        public string DetailType { get; set; }
    }
}
