/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Business.Interfaces
{
    public interface IItRequestFormBC : IRequestFormBC <ItRequestWorkflowInstance>
    {
    }
}
