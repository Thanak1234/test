using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.BatchData;
using Workflow.Domain.Entities.BatchData.EGMInstance;
using Workflow.Business.Interfaces;

namespace Workflow.Business.MCNRequestForm
{
    public interface IMCNRequestFormBC : IRequestFormBC<MCNRequestWorkflowInstance>
    {

    }
}
