using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Core;
namespace Workflow.DataAcess.Repositories.RequestApp
{
    public interface IRequestApplicationRepository
    {
        IEnumerable<RequestApplication> GetRequestApps();
        RequestApplication GetRequestApplicationByReqCode(string reqcode);
        RequestApplication GetRequestApplicationById(int id);
    }
}
