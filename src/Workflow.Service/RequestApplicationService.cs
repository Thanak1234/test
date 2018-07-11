using System.Collections.Generic;

using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.RequestApp;
using Workflow.Domain.Entities.Core;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{


    public class RequestApplicationService : IRequestApplicationService
    {

        private IRequestApplicationRepository rap;

        public RequestApplicationService()
        {
            rap = new RequestApplicationRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
        }

        public RequestApplication GetRequestApplicationById(int id)
        {
            return rap.GetRequestApplicationById(id);
        }

        public RequestApplication GetRequestApplicationByReqCode(string reqcode)
        {
            return rap.GetRequestApplicationByReqCode(reqcode);
        }

        public IEnumerable<RequestApplication> GetRequestApplicationList()
        {
            return rap.GetRequestApps();
        }

    }


}
