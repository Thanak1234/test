

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class RequestApplication
    {

        public int Id { get; set; }

        public string RequestCode { get; set; }

        public string RequestDesc { get; set; }

        public string ProcessName { get; set; }

        public string ProcessCode { get; set; }

        public string ProcessPath { get; set; }

        public string ReportPath { get; set; }

        public string FormUrl { get; set; }

        public string FormXType { get; set; }

        public int GenId { get; set; }

        public Nullable<int> IconIndex { get; set; }

        public Nullable<bool> Active { get; set; }

    }
}
