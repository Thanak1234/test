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
    public class TicketSubCategoryRepository : RepositoryBase<TicketSubCategory>, ITicketSubCategoryRepository
    {
        public TicketSubCategoryRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<TicketSubCategoryDto> getSubCategories(String query = null)
        {
            string sqlString = @"SELECT 
                          SC.[ID] id
                          ,SC.[CATE_ID] cateId
                          ,sc.[SUB_CATE_NAME] subCateName
                          ,sc.[DESCRIPTION] description
                          ,sc.[CREATED_DATE] createdDate
                          ,sc.[MODIFIED_DATE] modifiedDate
	                      ,c.CATE_NAME cateName
	                      ,c.[DESCRIPTION] cateDescription
                      FROM [TICKET].[SUB_CATEGORY] SC
                      INNER JOIN TICKET.CATEGORY C ON SC.CATE_ID=C.ID
                      WHERE 
	                    @query IS NULL OR (ISNULL(SC.SUB_CATE_NAME, '')+ ' '+ ISNULL(SC.[DESCRIPTION],'')+ ' '+ ISNULL(C.CATE_NAME,'')+ ' '+ ISNULL(C.[DESCRIPTION],'')) LIKE @query
                        ORDER BY sc.[SUB_CATE_NAME] "
                                ;
            object queryParam = "%" + query + "%";
            if (query == null)
                queryParam = DBNull.Value;

            return SqlQuery<TicketSubCategoryDto>(sqlString, new object[] { new SqlParameter("query", queryParam) }).ToList();
         
        }

        public Boolean isSubCategoryExisted(TicketSubCategory instance)
        {
            Boolean existed = false;
            var sqlString = @"SELECT count(*) FROM TICKET.SUB_CATEGORY A 
                    WHERE 
                    (@id > 0 OR (RTRIM(LTRIM(LOWER(A.SUB_CATE_NAME))) = RTRIM(LTRIM(LOWER(@name))) AND A.CATE_ID = @cateId))
                    AND (@id <= 0 OR (A.ID != @id AND (RTRIM(LTRIM(LOWER(A.SUB_CATE_NAME))) = RTRIM(LTRIM(LOWER(@name))) AND A.CATE_ID = @cateId)))";
            try
            {
                int total = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@id", instance.Id), new SqlParameter("@name", instance.SubCateName), new SqlParameter("@cateId", instance.CateId) }).FirstOrDefault<int>();
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

        public IEnumerable<TicketSubCategoryDto> getSubCategories(TicketSettingCriteria criteria)
        {
            string sqlString = @"SELECT 
                          SC.[ID] id
                          ,SC.[CATE_ID] cateId
                          ,sc.[SUB_CATE_NAME] subCateName
                          ,sc.[DESCRIPTION] description
                          ,sc.[CREATED_DATE] createdDate
                          ,sc.[MODIFIED_DATE] modifiedDate
	                      ,c.CATE_NAME cateName
	                      ,c.[DESCRIPTION] cateDescription
                            ,SC.STATUS status
                            ,L.ID statusId
                      FROM [TICKET].[SUB_CATEGORY] SC
                      INNER JOIN TICKET.CATEGORY C ON SC.CATE_ID=C.ID
                      INNER JOIN TICKET.LOOKUP L ON L.ACTIVE = 1 AND sc.STATUS = L.LOOKUP_CODE AND L.LOOKUP_KEY = 'SUBCATEGORY_STATUS'
                      WHERE 
	                    (@query IS NULL OR (ISNULL(SC.SUB_CATE_NAME, '')+ ' '+ ISNULL(SC.[DESCRIPTION],'')+ ' '+ ISNULL(C.CATE_NAME,'')+ ' '+ ISNULL(C.[DESCRIPTION],'')) LIKE @query)
                        AND (@status IS NULL OR @status = 'ALL' OR LOWER(SC.STATUS) = LOWER(@status))
                        ORDER BY sc.[SUB_CATE_NAME] "
                               ;
            object queryParam = "%" + criteria.query + "%";
            if (criteria.query == null)
                queryParam = DBNull.Value;

            object statusParam = criteria.status;
            if (criteria.status == null)
                statusParam = DBNull.Value;

            return SqlQuery<TicketSubCategoryDto>(sqlString, new object[] { new SqlParameter("query", queryParam), new SqlParameter("status", statusParam) }).ToList();
        }
    }
}
