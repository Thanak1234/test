﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.Interfaces;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Business.BcjRequestForm
{
    public interface IBcjRequestFormBC : IRequestFormBC<BcjRequestWorkflowInstance>
    {
    }
}
