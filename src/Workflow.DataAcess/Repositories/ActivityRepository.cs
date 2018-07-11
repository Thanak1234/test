using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Repositories {
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository {
        public ActivityRepository(IDbFactory dbFactory) : base(dbFactory) {
        }

        public string GetSubmissionConfig(string req, string activty) {
            return SqlQuery<string>(@"
                                    SELECT TOP 1 PROPERTY FROM BPMDATA.REQUEST_APPLICATION AP 
                                    INNER JOIN ADMIN.ACTIVITY A ON AP.ID = A.WORKFLOW_ID
                                    WHERE REQUEST_CODE = @REQ AND NAME = @Activity",
                                    new object[] {
                                        new SqlParameter("@REQ", req),
                                        new SqlParameter("@Activity", activty)
                                    }
                ).Single();
        }
    }
}
