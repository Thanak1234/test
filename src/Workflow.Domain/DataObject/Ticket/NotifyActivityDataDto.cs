﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class NotifyActivityDataDto : AbstractActivityViewDto
    {
        public int ticketId { get; set; }
        public string ticketNo { get; set; }
        
    }
}
