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
    public class TicketItemRepository : RepositoryBase<TicketItem>, ITicketItemRepository
    {
        public TicketItemRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<TicketItemDto> getTicketItems(string query)
        {
            string sqlString = @"SELECT 
                                   i.[ID] id
                                  ,i.[SUB_CATE_ID] subCateId
                                  ,i.[ITEM_NAME] itemName
                                  ,C.CATE_NAME+' -> '+sc.SUB_CATE_NAME+' -> '+ i.[ITEM_NAME] itemDisplayName
                                  ,i.[TEAM_ID] teamId
                                  ,i.[SLA_ID] slaId
                                  ,i.[DESCRIPTION] description
                                  ,i.[CREATED_DATE] createdDate
                                  ,i.[MODIFIED_DATE] modifiedDate
	                              ,sc.SUB_CATE_NAME subCateName
	                              ,sc.[DESCRIPTION] subCateDescription
                                  ,t.TEAM_NAME teamName
								  ,t.DESCRIPTION teamDescription
                                FROM TICKET.ITEM i
                                INNER JOIN TICKET.SUB_CATEGORY sc ON i.SUB_CATE_ID=sc.ID
                                INNER JOIN TICKET.CATEGORY C ON sc.CATE_ID = c.ID
                                INNER JOIN TICKET.TEAM t ON i.TEAM_ID = t.ID
                                WHERE @query IS NULL OR (
                                    ISNULL(i.ITEM_NAME, '')+ ' '+ ISNULL(i.[DESCRIPTION],'')
                                    + ' '+ ISNULL(sc.SUB_CATE_NAME,'')+ ' '+ ISNULL(sc.[DESCRIPTION],'')
                                    + ' '+ ISNULL(C.CATE_NAME,'')+ ' '+ ISNULL(C.[DESCRIPTION],'')
                                    + ' '+ ISNULL(t.TEAM_NAME,'')+ ' '+ ISNULL(t.[DESCRIPTION],'')
                                ) LIKE @query 
                                ORDER BY i.[ITEM_NAME]"
                                ;
            object queryParam = "%" + query + "%";
            if (query == null)
                queryParam = DBNull.Value;

            return SqlQuery<TicketItemDto>(sqlString, new object[] { new SqlParameter("query", queryParam) }).ToList();
        }

        public Boolean isItemExisted(TicketItem instance)
        {
            Boolean existed = false;
                var sqlString = @"SELECT count(*) FROM TICKET.ITEM A 
                    WHERE 
                    (@id > 0 OR (RTRIM(LTRIM(LOWER(A.ITEM_NAME))) = RTRIM(LTRIM(LOWER(@name))) AND A.SUB_CATE_ID = @subCateId AND A.TEAM_ID = @teamId))
                    AND (@id <= 0 OR (A.ID != @id AND (RTRIM(LTRIM(LOWER(A.ITEM_NAME))) = RTRIM(LTRIM(LOWER(@name))) AND A.SUB_CATE_ID = @subCateId AND A.TEAM_ID = @teamId)))";
            try
            {
                int total = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@id", instance.Id), new SqlParameter("@name", instance.ItemName), new SqlParameter("@subCateId", instance.SubCateId), new SqlParameter("@teamId", instance.TeamId) }).FirstOrDefault<int>();
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

        public IEnumerable<TicketItemDto> getTicketItems(TicketSettingCriteria criteria)
        {
            string sqlString = @"SELECT 
                                   i.[ID] id
                                  ,i.[SUB_CATE_ID] subCateId
                                  ,i.[ITEM_NAME] itemName
                                  ,C.CATE_NAME+' -> '+sc.SUB_CATE_NAME+' -> '+ i.[ITEM_NAME] itemDisplayName
                                  ,i.[TEAM_ID] teamId
                                  ,i.[SLA_ID] slaId
                                  ,i.[DESCRIPTION] description
                                  ,i.[CREATED_DATE] createdDate
                                  ,i.[MODIFIED_DATE] modifiedDate
	                              ,sc.SUB_CATE_NAME subCateName
	                              ,sc.[DESCRIPTION] subCateDescription
                                  ,t.TEAM_NAME teamName
								  ,t.DESCRIPTION teamDescription
                                ,i.STATUS status
                                ,L.ID statusId
                                FROM TICKET.ITEM i
                                INNER JOIN TICKET.SUB_CATEGORY sc ON i.SUB_CATE_ID=sc.ID
                                INNER JOIN TICKET.CATEGORY C ON sc.CATE_ID = c.ID
                                INNER JOIN TICKET.TEAM t ON i.TEAM_ID = t.ID
                                INNER JOIN TICKET.LOOKUP L ON L.ACTIVE = 1 AND i.STATUS = L.LOOKUP_CODE AND L.LOOKUP_KEY = 'ITEM_STATUS'
                                WHERE (@query IS NULL OR (
                                    ISNULL(i.ITEM_NAME, '')+ ' '+ ISNULL(i.[DESCRIPTION],'')
                                    + ' '+ ISNULL(sc.SUB_CATE_NAME,'')+ ' '+ ISNULL(sc.[DESCRIPTION],'')
                                    + ' '+ ISNULL(C.CATE_NAME,'')+ ' '+ ISNULL(C.[DESCRIPTION],'')
                                    + ' '+ ISNULL(t.TEAM_NAME,'')+ ' '+ ISNULL(t.[DESCRIPTION],'')
                                ) LIKE @query)
                                AND (@status IS NULL OR @status = 'ALL' OR LOWER(i.STATUS) = LOWER(@status))
                                ORDER BY i.[ITEM_NAME]"
                                   ;
            object queryParam = "%" + criteria.query + "%";
            if (criteria.query == null)
                queryParam = DBNull.Value;

            object statusParam = criteria.status;
            if (criteria.status == null)
                statusParam = DBNull.Value;

            return SqlQuery<TicketItemDto>(sqlString, new object[] { new SqlParameter("query", queryParam), new SqlParameter("status", statusParam) }).ToList();

        }
    }
}
