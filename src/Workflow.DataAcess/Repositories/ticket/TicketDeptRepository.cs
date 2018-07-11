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
    public class TicketDeptRepository : RepositoryBase<TicketDepartment>, ITicketDeptRepository
    {
        public TicketDeptRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public IEnumerable<TicketDepartmentDto> getDepartments(string query = null)
        {
            string sqlString = @"SELECT
                        D.ID id
                        ,D.DEPT_NAME deptName
                        ,D.AUTOMATION_EMIAL automationEmail
                        ,D.DESCRIPTION description
                        ,D.[CREATED_DATE] createdDate
                        ,D.[MODIFIED_DATE] modifiedDate
                        ,D.[DEFAULT_ITEM_ID] defaultItemId
                        FROM TICKET.DEPARTMENT D
                        WHERE
                        @query IS NULL OR (ISNULL(d.DEPT_NAME, '')+ ' '+ ISNULL(D.[DESCRIPTION],'')+ ' '+ ISNULL(D.AUTOMATION_EMIAL,'')) LIKE @query
                        ORDER BY D.DEPT_NAME "
                               ;

            object queryParam = "%" + query + "%";
            if (query == null)
                queryParam = DBNull.Value;

            return SqlQuery<TicketDepartmentDto>(sqlString, new object[] { new SqlParameter("query", queryParam) }).ToList();
        }

        public Boolean isDepartmentExisted(TicketDepartment instance)
        {
            Boolean existed = false;
            var sqlString = @"SELECT count(*) FROM TICKET.DEPARTMENT A 
                WHERE 
                (@id > 0 OR (RTRIM(LTRIM(LOWER(A.DEPT_NAME))) = RTRIM(LTRIM(LOWER(@name))) OR RTRIM(LTRIM(LOWER(A.AUTOMATION_EMIAL))) = RTRIM(LTRIM(LOWER(@email)))))
                AND (@id <= 0 OR (A.ID != @id AND (RTRIM(LTRIM(LOWER(A.DEPT_NAME))) = RTRIM(LTRIM(LOWER(@name))) OR RTRIM(LTRIM(LOWER(A.AUTOMATION_EMIAL))) = RTRIM(LTRIM(LOWER(@email))))))";
            try
            {
                int total = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@id", instance.Id), new SqlParameter("@name", instance.DeptName), new SqlParameter("@email", instance.AutomationEmail) }).FirstOrDefault<int>();
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

        public IEnumerable<TicketDepartmentDto> getDepartments(TicketSettingCriteria criteria)
        {
            string sqlString = @"SELECT
                        D.ID id
                        ,D.DEPT_NAME deptName
                        ,D.AUTOMATION_EMIAL automationEmail
                        ,D.DESCRIPTION description
                        ,D.[CREATED_DATE] createdDate
                        ,D.[MODIFIED_DATE] modifiedDate
                        ,D.[DEFAULT_ITEM_ID] defaultItemId
                        ,D.STATUS status
                        ,L.ID statusId
                        FROM TICKET.DEPARTMENT D
                        INNER JOIN TICKET.LOOKUP L ON L.ACTIVE = 1 AND D.STATUS = L.LOOKUP_CODE AND L.LOOKUP_KEY = 'DEPARTMENT_STATUS'
                        WHERE
                        (@query IS NULL OR (ISNULL(d.DEPT_NAME, '')+ ' '+ ISNULL(D.[DESCRIPTION],'')+ ' '+ ISNULL(D.AUTOMATION_EMIAL,'')) LIKE @query)
                        AND (@status IS NULL OR @status = 'ALL' OR LOWER(D.STATUS) = LOWER(@status))
                        ORDER BY D.DEPT_NAME "
                               ;

            object queryParam = "%" + criteria.query + "%";
            if (criteria.query == null)
                queryParam = DBNull.Value;

            object statusParam = criteria.status;
            if (criteria.status == null)
                statusParam = DBNull.Value;

            return SqlQuery<TicketDepartmentDto>(sqlString, new object[] { new SqlParameter("query", queryParam), new SqlParameter("status", statusParam) }).ToList();
        }

    }
}
