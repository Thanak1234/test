

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class NotificationUserSession
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string ConnectionId { get; set; }

        public Nullable<int> NotificationCount { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}
