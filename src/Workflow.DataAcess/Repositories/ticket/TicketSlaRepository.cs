using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public class TicketSlaRepository : RepositoryBase<TicketSLA>, ITicketSlaRepository
    {
        public TicketSlaRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<TicketSlaDto> getSlas(string query = null)
        {
            string sqlString = @"SELECT SC.[ID] id
                      ,SC.SLA_NAME slaName
					  ,SC.GRACE_PERIOD gracePeriod
					  ,SC.[FIRST_RESPONSE_PERIOD] firstResponsePeriod					  
                      ,SC.[DESCRIPTION] description
                      ,SC.[CREATED_DATE] createdDate
                      ,SC.[MODIFIED_DATE] modifiedDate
	                  ,L.[LOOKUP_NAME] statusName
                      ,SC.[STATUS] status
                  FROM [TICKET].[SLA] SC
                  INNER JOIN TICKET.[LOOKUP] L ON L.ACTIVE = 1 AND L.LOOKUP_KEY='SLA_STATUS' AND L.LOOKUP_CODE = SC.[STATUS]
                  WHERE 
	                @query IS NULL OR (
						ISNULL(SC.SLA_NAME, '') + ' '+ 
						ISNULL(SC.[DESCRIPTION],'')+ ' '												
						+ ISNULL(L.LOOKUP_NAME,'')
					) LIKE @query 
                    ORDER BY SC.SLA_NAME"
                                ;

            object queryParam = "%" + query + "%";
            if (query == null)
                queryParam = DBNull.Value;

            return SqlQuery<TicketSlaDto>(sqlString, new object[] { new SqlParameter("query", queryParam) }).ToList();
        }

        public Boolean isSlaExisted(TicketSLA instance)
        {
            Boolean existed = false;
            var sqlString = @"SELECT count(*) FROM TICKET.SLA A 
                    WHERE 
                    (@id > 0 OR RTRIM(LTRIM(LOWER(A.SLA_NAME))) = RTRIM(LTRIM(LOWER(@name))))
                    AND (@id <= 0 OR (A.ID != @id AND RTRIM(LTRIM(LOWER(A.SLA_NAME))) = RTRIM(LTRIM(LOWER(@name)))))";
            try
            {
                int total = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@id", instance.Id), new SqlParameter("@name", instance.SlaName) }).FirstOrDefault<int>();
                if (total > 0)
                {
                    existed = true;
                }
            }
            catch (Exception e)
            {
                Console.Write("e ", e.InnerException);
            }
            return existed;
        }

    }
}
