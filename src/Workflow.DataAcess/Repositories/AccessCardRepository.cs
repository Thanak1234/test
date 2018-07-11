using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.Dashboard;
using Workflow.Domain.Entities.RAC;

namespace Workflow.DataAcess.Repositories {
    public class AccessCardRepository : RepositoryBase<AccessCard>, IAccessCardRepository {    
        public AccessCardRepository(IDbFactory dbFactory) : base(dbFactory) {
            
        }
    }
}
