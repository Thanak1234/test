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
    public class TicketSlaMappingRepository : RepositoryBase<TicketSLAMapping>, ITicketSlaMappingRepository
    {
        public TicketSlaMappingRepository(IDbFactory dbFactory) : base(dbFactory) { }
        
        public IEnumerable<TicketSlaMappingDto> getSlasMapping(string query = null)
        {
            string sqlString = @"SELECT SM.ID id,
		            P.PRIORITY_NAME priority,
		            S.SLA_NAME sla,
		            T.[TYPE_NAME] ticketType,
                    SM.PRIORITY_ID priorityId,
                    SM.SLA_ID slaId,
                    SM.TYPE_ID typeId,
                    SM.DESCRIPTION description
                  FROM TICKET.SLA_MAPPING SM (NOLOCK)
				  INNER JOIN TICKET.[PRIORITY] P (NOLOCK) ON SM.PRIORITY_ID = P.ID
				  INNER JOIN TICKET.[SLA] S (NOLOCK) ON SM.SLA_ID = S.ID
				  INNER JOIN TICKET.[TYPE] T (NOLOCK) ON SM.[TYPE_ID] = T.ID
                  WHERE 
	                @query IS NULL OR (
						ISNULL(S.SLA_NAME, '') + ' '+ 
						ISNULL(S.[DESCRIPTION],'')+ ' '+												
						ISNULL(T.[TYPE_NAME], '') + ' '+ 
						ISNULL(T.[DESCRIPTION],'')+ ' '+												
						ISNULL(P.[DESCRIPTION],'')+ ' '	+											
						ISNULL(P.PRIORITY_NAME,'')
					) LIKE @query 
                    ORDER BY T.[TYPE_NAME]"
                                ;

            object queryParam = "%" + query + "%";
            if (query == null)
                queryParam = DBNull.Value;

            return SqlQuery<TicketSlaMappingDto>(sqlString, new object[] { new SqlParameter("query", queryParam) }).ToList();
        }

        public Boolean isSlaMappingExisted(TicketSLAMapping instance)
        {
            Boolean existed = false;
            var sqlString = @"SELECT count(*) FROM TICKET.SLA_MAPPING A 
                    WHERE 
                    (@id > 0 OR (A.TYPE_ID = @typeId  AND A.PRIORITY_ID = @priorityId))
                    AND (@id <= 0 OR (A.ID != @id AND A.TYPE_ID = @typeId AND A.PRIORITY_ID = @priorityId ))";
            try
            {
                int total = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@id", instance.Id), new SqlParameter("@typeId", instance.TypeId), new SqlParameter("@slaId", instance.SlaId), new SqlParameter("@priorityId", instance.PriorityId) }).FirstOrDefault<int>();
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
