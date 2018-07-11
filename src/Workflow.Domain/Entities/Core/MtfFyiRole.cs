

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class MtfFyiRole
    {

        public int Id { get; set; }

        public string Guid { get; set; }

        public string Fqn { get; set; }

        public string Description { get; set; }

    }
}
