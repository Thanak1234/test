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
    public class TicketPriorityRepository : RepositoryBase<TicketPriority>, ITicketPriorityRepository
    {
        public TicketPriorityRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<TicketPriorityDto> getPriorities(string query = null)
        {
            string sqlString = @"SELECT SC.[ID] id                      
                              ,SC.PRIORITY_NAME priorityName
                              ,SC.[DESCRIPTION] description
                              ,SC.[CREATED_DATE] createdDate
                              ,SC.[MODIFIED_DATE] modifiedDate
							  ,S.SLA_NAME slaName
							  ,S.ID slaId
							  ,S.[DESCRIPTION] slaDescription							 	                  
                          FROM [TICKET].[PRIORITY] sc
						  INNER JOIN TICKET.SLA S ON SC.SLA_ID = S.ID                  
                          WHERE 
	                        @query IS NULL OR (
							ISNULL(SC.PRIORITY_NAME, '')+ ' '+ 
							ISNULL(S.SLA_NAME, '')+ ' '+ 
							ISNULL(S.[DESCRIPTION], '')+ ' '+ 
							ISNULL(SC.[DESCRIPTION],'')
							) LIKE @query 
                            ORDER BY SC.PRIORITY_NAME"
                                ;

            object queryParam = "%" + query + "%";
            if (query == null)
                queryParam = DBNull.Value;

            return SqlQuery<TicketPriorityDto>(sqlString, new object[] { new SqlParameter("query", queryParam) }).ToList();
        }

        public Boolean isPriorityExisted(TicketPriority instance)
        {
            Boolean existed = false;
            var sqlString = @"SELECT count(*) FROM TICKET.[PRIORITY] A 
                    WHERE 
                    (@id > 0 OR (RTRIM(LTRIM(LOWER(A.PRIORITY_NAME))) = RTRIM(LTRIM(LOWER(@name))) AND A.SLA_ID = @slaId))
                    AND (@id <= 0 OR (A.ID != @id AND (RTRIM(LTRIM(LOWER(A.PRIORITY_NAME))) = RTRIM(LTRIM(LOWER(@name))) AND A.SLA_ID = @slaId)))";
            try
            {
                int total = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@id", instance.Id), new SqlParameter("@name", instance.PriorityName), new SqlParameter("@slaId", instance.SlaId) }).FirstOrDefault<int>();
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
