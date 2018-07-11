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
    public class TicketCategoryRepository : RepositoryBase<TicketCategory>, ITicketCategoryRepository
    {
        public TicketCategoryRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<TicketCategoryDto> getCategories(string query = null)
        {
            string sqlString = @"SELECT SC.[ID] id
                      ,SC.[DEPT_ID] deptId
                      ,SC.[CATE_NAME] cateName
                      ,SC.[DESCRIPTION] description
                      ,SC.[CREATED_DATE] createdDate
                      ,SC.[MODIFIED_DATE] modifiedDate
	                  ,D.DEPT_NAME deptName
	                  ,D.DESCRIPTION deptDescription
                  FROM [TICKET].[CATEGORY] sc
                  INNER JOIN TICKET.DEPARTMENT D ON SC.DEPT_ID = D.ID
                  WHERE 
	                @query IS NULL OR (ISNULL(SC.CATE_NAME, '')+ ' '+ ISNULL(SC.[DESCRIPTION],'')+ ' '+ ISNULL(D.DEPT_NAME,'')+ ' '+ ISNULL(D.DESCRIPTION,'')) LIKE @query 
                    ORDER BY SC.[CATE_NAME]"
                                ;

            object queryParam = "%" + query + "%";
            if (query == null)
                queryParam = DBNull.Value;

            return SqlQuery<TicketCategoryDto>(sqlString, new object[] { new SqlParameter("query", queryParam) }).ToList();
        }

        public Boolean isCategoryExisted(TicketCategory instance)
        {
            Boolean existed = false;
            var sqlString = @"SELECT count(*) FROM TICKET.CATEGORY A 
                    WHERE 
                    (@id > 0 OR (RTRIM(LTRIM(LOWER(A.CATE_NAME))) = RTRIM(LTRIM(LOWER(@name)))) AND A.DEPT_ID = @deptId)
                    AND (@id <= 0 OR (A.ID != @id AND (RTRIM(LTRIM(LOWER(A.CATE_NAME))) = RTRIM(LTRIM(LOWER(@name)))) AND A.DEPT_ID = @deptId))";
            try
            {
                int total = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@id", instance.Id), new SqlParameter("@name", instance.CateName), new SqlParameter("@deptId", instance.DeptId) }).FirstOrDefault<int>();
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

        public IEnumerable<TicketCategoryDto> getCategories(TicketSettingCriteria criteria)
        {
            string sqlString = @"SELECT SC.[ID] id
                      ,SC.[DEPT_ID] deptId
                      ,SC.[CATE_NAME] cateName
                      ,SC.[DESCRIPTION] description
                      ,SC.[CREATED_DATE] createdDate
                      ,SC.[MODIFIED_DATE] modifiedDate
	                  ,D.DEPT_NAME deptName
	                  ,D.DESCRIPTION deptDescription
                      ,sc.STATUS status
                      ,L.ID statusId
                  FROM [TICKET].[CATEGORY] sc
                  INNER JOIN TICKET.DEPARTMENT D ON SC.DEPT_ID = D.ID
                    INNER JOIN TICKET.LOOKUP L ON L.ACTIVE = 1 AND sc.STATUS = L.LOOKUP_CODE AND L.LOOKUP_KEY = 'CATEGORY_STATUS'
                  WHERE 
	                (@query IS NULL OR (ISNULL(SC.CATE_NAME, '')+ ' '+ ISNULL(SC.[DESCRIPTION],'')+ ' '+ ISNULL(D.DEPT_NAME,'')+ ' '+ ISNULL(D.DESCRIPTION,'')) LIKE @query )
                    AND (@status IS NULL OR @status = 'ALL' OR LOWER(sc.STATUS) = LOWER(@status))
                    ORDER BY SC.[CATE_NAME]"
                               ;

            object queryParam = "%" + criteria.query + "%";
            if (criteria.query == null)
                queryParam = DBNull.Value;

            object statusParam = criteria.status;
            if (criteria.status == null)
                statusParam = DBNull.Value;

            return SqlQuery<TicketCategoryDto>(sqlString, new object[] { new SqlParameter("query", queryParam), new SqlParameter("status", statusParam) }).ToList();
        }
    }
}
