

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Scheduler
{
    public class Job
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string CronExpression { get; set; }

        public bool IsActive { get; set; }

        public int Status { get; set; }

        public string KeyValue { get; set; }

    }
}
