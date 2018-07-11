using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Core;
using Workflow.Service.Interfaces.Admin;

namespace Workflow.Service.Admin {

    public class ApplicationService: ServiceBase, IApplicationService {

        public ApplicationService() {
            
        }

        public RequestApplication AddApplication(RequestApplication entity) {
            Add(entity);
            return entity;
        }

        public void DeleteApplication(RequestApplication entity) {
            Delete(entity);
        }

        public IEnumerable<RequestApplication> GetApplications() {
            return GetAll<RequestApplication>();
        }

        public RequestApplication UpdateApplication(RequestApplication entity) {
            Update(entity);
            return entity;
        }

        public RequestApplication GetSingle(int id) {
            return GetById<RequestApplication>(id);
        }
    }

}
