using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.Interfaces;
using Workflow.Domain.Entities.BatchData;
using Workflow.Domain.Entities.BatchData.EGMInstance;

namespace Workflow.Business.ATDRequestForm
{
    public interface IATDRequestFormBC : IRequestFormBC<ATDRequestWorkflowInstance>
    {

    }
}
