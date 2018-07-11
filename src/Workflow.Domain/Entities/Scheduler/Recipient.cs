

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Scheduler
{
    public class Recipient {

        public int Id { get; set; }

        public int EmailId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

    }
}
