using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.Interfaces;
using Workflow.Domain.WorkflowData;

namespace Workflow.Business.AVRequestForm
{
    public interface IBcjRequestFormBC : IRequestFormBC<BcjRequestWorkflowInstance>
    {
    }
}
