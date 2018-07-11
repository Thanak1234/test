/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.Domain.Entities;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class RequestHeaderService : IRequestHeaderService
    {
        private readonly IRequestHeaderRepository requestHeaderRepo;


        public RequestHeaderService()
        {
            IDbFactory dbFactory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            requestHeaderRepo = new RequestHeaderRepository(dbFactory);
        }

        public RequestHeader GetRequestHeader(int id)
        {
            return requestHeaderRepo.GetById(id);
        }

        public void SaveRequestHeader(RequestHeader requestHeader)
        {
            throw new NotImplementedException();
        }

        public object GetIncidentRequestHeaders(QueryParameter queryParam) {
            var queryable = requestHeaderRepo.GetMany(p => p.RequestCode.Equals(Business.PROCESSCODE.EGMIR) && (string.IsNullOrEmpty(queryParam.query) || p.Title.Contains(queryParam.query)));
            int totalCount = queryable.Count();
            var records = queryable.Skip(queryParam.start)
                            .Take(queryParam.limit)
                            .Select(x => {
                                object obj = new {
                                    Title = x.Title,
                                    RouteId = string.Format("#icd-request-form/SN={0}_99999", x.ProcessInstanceId, x.Title),
                                    ProcessInstanceId = x.ProcessInstanceId,
                                    Id = x.Id,
                                    Requestor = x.Requestor.DisplayName
                                };
                                return obj;
                            })
                            .ToList();
            return new { totalCount = totalCount, data = records };
        }

        public object GetFormsByREQ(QueryParameter queryParam, string requestCode) {
            var query = requestHeaderRepo.GetMany(p => p.RequestCode.Equals(requestCode) && p.ProcessInstanceId > 0
            && (string.IsNullOrEmpty(queryParam.query) 
            || p.Title.Contains(queryParam.query)));

            int totalCount = query.Count();
            var records = query.Skip(queryParam.start)
                            .Take(queryParam.limit)
                            .Select(x => {
                                return new {
                                    ProcId = x.ProcessInstanceId,
                                    Folio = x.Title,
                                    Requestor = x.Requestor.DisplayName
                                };
                            })
                            .ToList();
            return new { totalCount = totalCount, data = records };
        }
    }
}
