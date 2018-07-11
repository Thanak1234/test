using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.HR;

namespace Workflow.DataAcess.Repositories.HumanResource
{
    public class FlightDetailRepository : RepositoryBase<FlightDetail>, IFlightDetailRepository
    {
        public FlightDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public int GetTakenYears(int requestorId, int purposeId) {

            string sql = @"
                            SELECT COUNT(*) Token FROM
                            (
	                            SELECT D.REQUEST_HEADER_ID, MIN([DATE]) MIN_DATE
	                            FROM [HR].[DESTINATION] D
	                            INNER JOIN BPMDATA.REQUEST_HEADER R ON D.REQUEST_HEADER_ID = R.ID
	                            INNER JOIN [HR].[TRAVEL_DETAIL] TR ON R.ID = TR.REQUEST_HEADER_ID
	                            WHERE R.REQUESTOR = @RequestorId AND YEAR([DATE]) = YEAR(GETDATE()) AND R.STATUS = 'Done' AND TR.PURPOSE_TRAVEL = @PurposeId
	                            GROUP BY D.REQUEST_HEADER_ID
                            ) T
                        ";

            int token = DbContext.Database.SqlQuery<int>(sql, new object[] {
                new SqlParameter("@RequestorId", requestorId),
                new SqlParameter("@PurposeId", purposeId)
            }).FirstOrDefault();

            return token;
        }
    }
}
