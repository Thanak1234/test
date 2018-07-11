using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.VoucherRequest;

namespace Workflow.DataAcess.Repositories.VoucherRequest {
    public class RequestDataRepository : RepositoryBase<RequestData>, IRequestDataRepository {
        public RequestDataRepository(IDbFactory dbFactory) : base(dbFactory) {
        }

        public int GetTotalQty() {
            string sql = @"SELECT ISNULL(SUM(RD.QTY_REQUEST),0) QTY FROM BPMDATA.REQUEST_HEADER RH 
                            INNER JOIN FINANCE.VR_REQUEST_DATA RD ON RH.ID = RD.REQUEST_HEADER_ID
                            WHERE YEAR(RH.SUBMITTED_DATE) = YEAR(GETDATE())";
            return DbContext.Database.SqlQuery<int>(sql).FirstOrDefault();
        }
    }
}
