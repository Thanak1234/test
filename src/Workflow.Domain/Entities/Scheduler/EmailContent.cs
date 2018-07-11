

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Scheduler
{
    public class EmailContent {

        public int Id { get; set; }

        public int JobId { get; set; }

        public string ContentType { get; set; }

        public string MessageBody { get; set; }

        public string Subject { get; set; }

    }
}
