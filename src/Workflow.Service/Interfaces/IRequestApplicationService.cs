using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Core;

namespace Workflow.Service.Interfaces
{
    public interface IRequestApplicationService
    {
        RequestApplication GetRequestApplicationById(int id);
        RequestApplication GetRequestApplicationByReqCode(string reqcode);
        IEnumerable<RequestApplication> GetRequestApplicationList();
    }
}
