/*
Author : Chandara
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.Interfaces;
using Workflow.Domain.Entities.BatchData;
using Workflow.Domain.Entities.BatchData.IncidentInstance;
namespace Workflow.Business.ICDRequestForm
{
    public interface IICDRequestFormBC : IRequestFormBC<ICDRequestWorkflowInstance>
    {

    }
}
