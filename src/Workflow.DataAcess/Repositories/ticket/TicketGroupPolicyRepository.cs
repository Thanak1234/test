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
    public class TicketGroupPolicyRepository : RepositoryBase<TicketGroupPolicy>,ITicketGroupPolicyReplository
    {
        public TicketGroupPolicyRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<TicketGroupPolicyDto> getGroupPolicies(string query = null)
        {
            string sqlString = @"SELECT 
                            GP.[ID] id      
                            ,GP.GROUP_NAME groupName
                            ,GP.[DESCRIPTION] description
                            ,GP.[CREATED_DATE] createdDate
                            ,GP.[MODIFIED_DATE] modifiedDate                            
                            ,S.ID statusId
                            ,S.LOOKUP_NAME status
                            ,LA.ID limitAccessId
                            ,LA.LOOKUP_NAME limitAccess
                            ,TN.ID assignedNotifyId
                            ,TN.LOOKUP_NAME assignedNotify
                            ,AN.ID replyNotifyId
                            ,AN.LOOKUP_NAME replyNotify
                            ,RN.ID changeStatusNotifyId
                            ,RN.LOOKUP_NAME changeStatusNotify
                            ,CSN.ID newTicketNotifyId
                            ,CSN.LOOKUP_NAME newTicketNotify
                            ,[CREATE_TICKET] createTicket
                            ,[EDIT_TICKET] editTicket
                            ,[EDIT_REQUESTOR] editRequestor
                            ,[POST_TICKET] postTicket
                            ,[CLOSE_TICKET] closeTicket
                            ,[ASSIGN_TICKET] assignTicket
                            ,[MERGE_TICKET] mergeTicket
                            ,[DELETE_TICKET] deleteTicket
                            ,[DEPT_TRANSFER] deptTransfer
                            ,[CHANGE_STATUS] changeStatus
                            ,[CREATE_SUB_TICKET] subTicket
                            ,RA.LOOKUP_NAME reportAccess
                            ,RA.ID reportAccessId
                            FROM TICKET.GROUP_POLICY GP
                            INNER JOIN TICKET.[LOOKUP] S ON S.LOOKUP_KEY='GROUP_POLICY_STATUS' AND GP.STATUS=S.LOOKUP_CODE 
                            INNER JOIN TICKET.[LOOKUP] LA ON LA.LOOKUP_KEY='GROUP_POLICY_LIMIT_ACCESS' AND GP.LIMIT_ACCESS=LA.LOOKUP_CODE
                            INNER JOIN TICKET.[LOOKUP] TN ON TN.LOOKUP_KEY='GROUP_POLICY_ASSIGNED_NOTIFY' AND GP.ASSIGNED_NOTIFY=TN.LOOKUP_CODE 
                            INNER JOIN TICKET.[LOOKUP] AN ON AN.LOOKUP_KEY='GROUP_POLICY_REPLY_NOTIFY' AND GP.REPLY_NOTIFY=AN.LOOKUP_CODE 
                            INNER JOIN TICKET.[LOOKUP] RN ON RN.LOOKUP_KEY='GROUP_POLICY_CHANGE_STATUS_NOTIFY' AND GP.CHANGE_STATUS_NOTIFY=RN.LOOKUP_CODE 
                            INNER JOIN TICKET.[LOOKUP] CSN ON CSN.LOOKUP_KEY='GROUP_POLICY_NEW_TICKET_NOTIFY' AND GP.NEW_TICKET_NOTIFY=CSN.LOOKUP_CODE
                            INNER JOIN TICKET.[LOOKUP] RA ON RA.LOOKUP_KEY='GROUP_POLICY_REPORT_ACCESS' AND GP.REPORT_ACCESS=RA.LOOKUP_CODE
                            WHERE 
                            @query IS NULL OR (
	                            ISNULL(GP.[GROUP_NAME],'')+' '+
	                            ISNULL(GP.[DESCRIPTION],'')+' '+
	                            ISNULL(S.LOOKUP_NAME,'')+' '+
	                            ISNULL(LA.LOOKUP_NAME,'')+' '+
	                            ISNULL(TN.LOOKUP_NAME,'')+' '+
	                            ISNULL(AN.LOOKUP_NAME,'')+' '+
	                            ISNULL(RN.LOOKUP_NAME,'')+' '+
                                ISNULL(RA.LOOKUP_NAME,'')+' '+
	                            ISNULL(CSN.LOOKUP_NAME,'')

                            ) LIKE @query 
                            ORDER BY GP.GROUP_NAME "
                                ;

            object queryParam = "%" + query + "%";
            if (query == null)
                queryParam = DBNull.Value;

            return SqlQuery<TicketGroupPolicyDto>(sqlString, new object[] { new SqlParameter("query", queryParam) }).ToList();
        }

        public IEnumerable<TicketGroupPolicyTeamsDto> getTeams(int groupPolicyId = 0)
        {
            string sqlString = @"SELECT T.[ID] id      
                                , T.TEAM_NAME teamName
                                ,T.[DESCRIPTION] description
                                ,T.[CREATED_DATE] createdDate
                                ,T.[MODIFIED_DATE] modifiedDate	                           	                            
                                ,T.DIRECTORY_LISTING directoryListing
                                ,GP.ID groupPolicyId         
                            FROM TICKET.TEAM T
                            INNER JOIN TICKET.[TEAM_GROUP_ACCESS] TGA ON T.ID = TGA.TEAM_ID
                            INNER JOIN TICKET.GROUP_POLICY GP ON TGA.GROUP_ACCESS_ID = GP.ID
                            
                            WHERE                                                         
                            TGA.STATUS = 'ACTIVE'
                            AND (@grouppolicyId > 0 AND GP.ID = @grouppolicyId)
                            ORDER BY T.TEAM_NAME "
                                ;

            

            return SqlQuery<TicketGroupPolicyTeamsDto>(sqlString, new object[] { new SqlParameter("grouppolicyId", groupPolicyId) }).ToList();
        }

        public IEnumerable<TicketGroupPolicyTeamsDto> getReportAccessTeams(int groupPolicyId = 0)
        {
            string sqlString = @"SELECT T.[ID] id      
                                , T.TEAM_NAME teamName
                                ,T.[DESCRIPTION] description
                                ,T.[CREATED_DATE] createdDate
                                ,T.[MODIFIED_DATE] modifiedDate	                           	                            
                                ,T.DIRECTORY_LISTING directoryListing
                                ,GP.ID groupPolicyId         
                            FROM TICKET.TEAM T
                            INNER JOIN TICKET.[TEAM_REPORT_ACCESS] TGA ON T.ID = TGA.TEAM_ID
                            INNER JOIN TICKET.GROUP_POLICY GP ON TGA.GROUP_ACCESS_ID = GP.ID
                            
                            WHERE                                                         
                            TGA.STATUS = 'ACTIVE'
                            AND (@grouppolicyId > 0 AND GP.ID = @grouppolicyId)
                            ORDER BY T.TEAM_NAME "
                               ;



            return SqlQuery<TicketGroupPolicyTeamsDto>(sqlString, new object[] { new SqlParameter("grouppolicyId", groupPolicyId) }).ToList();
        }

        public Boolean isGroupPoliciesExisted(TicketGroupPolicyDto instance)
        {
            Boolean existed = false;
            var sqlString = @"SELECT count(*) FROM TICKET.GROUP_POLICY A 
                    WHERE 
                    (@id > 0 OR RTRIM(LTRIM(LOWER(A.GROUP_NAME))) = RTRIM(LTRIM(LOWER(@name))))
                    AND (@id <= 0 OR (A.ID != @id AND RTRIM(LTRIM(LOWER(A.GROUP_NAME))) = RTRIM(LTRIM(LOWER(@name)))))";
            try
            {
                int total = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@id", instance.id), new SqlParameter("@name", instance.groupName) }).FirstOrDefault<int>();
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
