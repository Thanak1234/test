using System.Collections.Generic;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Repositories.RequestApp
{
    public class RequestApplicationRepository : RepositoryBase<RequestApplication>, IRequestApplicationRepository
    {
        public RequestApplicationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public RequestApplication GetRequestApplicationById(int id)
        {
            var context = DbContext as WorkflowContext;

            return context.RequestApplications.Where(n => n.Id == id).SingleOrDefault();
        }

        public RequestApplication GetRequestApplicationByReqCode(string reqcode)
        {
            var context = DbContext as WorkflowContext;

            return context.RequestApplications.Where(n => n.RequestCode == reqcode).SingleOrDefault();
        }

        public IEnumerable<RequestApplication> GetRequestApps()        {
            var context = DbContext as WorkflowContext;
            return context.RequestApplications.Where(x => x.Active == true).OrderBy(p => p.ProcessName);
        }
    }
}
