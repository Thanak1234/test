

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class RequestExported
    {

        public int RequestHeaderId { get; set; }

        public string RequestCode { get; set; }

        public bool Exported { get; set; }

    }
}
