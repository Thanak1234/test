﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketRouteStatus : AbstractEntityLevel2
    {
        public String Status { get; set; }
    }
}
