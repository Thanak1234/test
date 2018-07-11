/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.Scheduler;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Dto;

namespace Workflow.DataAcess.Repositories
{
    public class RequestHeaderRepository : RepositoryBase<RequestHeader>, IRequestHeaderRepository
    {
        public RequestHeaderRepository(IDbFactory dbFactory) : base(dbFactory){}

        public bool Editable(int requestHeaderId, string requestCode, string currentUser)
        {
            var editable = Single<EditableRequest>("[BPMDATA].[EDITABLE_REQ_FORM] @requestCode,@currUser, @requestHeaderId  ", new SqlParameter("@requestCode", requestCode), new SqlParameter("@currUser", currentUser), new SqlParameter("@requestHeaderId", requestHeaderId));
            return editable.Editable;
        }


        public string GetRequestNo(string prefix, string requestCode)
        {
            var next = Single<NextID>("GenID @RequestCode  ", new SqlParameter("@RequestCode", requestCode));

            var requestNo= String.Format("{0}-{1:D6}", prefix, next.nextID);
            return requestNo;

        }

        public WorkflowStatus GetWorkflowStatus(int requestHeaderId)
        {
            var record = Single<WorkflowStatus>("select * from  [dbo].[getWorkflowStaus](@requestHeaderId)", new SqlParameter("@requestHeaderId", requestHeaderId));
            return record;
        }

        public Originator GetRequestorEmail(string loginName)
        {
            var email = Single<Originator>("[HR].[GET_ORIGINATOR] @LoginName", new SqlParameter("@LoginName", loginName));
            return email;
        }

        public List<string> GetEmailsByRole(string[] roleNames)
        {
            string sqlString = @"SELECT DISTINCT E.EMAIL FROM [Workflow].[K2].USER_ROLE R
                INNER JOIN HR.EMPLOYEE E ON E.LOGIN_NAME = REPLACE(R.LOGIN_NAME, 'K2:', '')
                WHERE E.EMAIL IS NOT NULL AND R.ROLE_NAME IN('" + string.Join("', '", roleNames) + "')";
            var emailList = SqlQuery<string>(sqlString);
            return emailList.ToList();
        }

        private class NextID
        {
            public int nextID { get; set; }
        }

        private class EditableRequest
        {
            public bool Editable { get; set; }
        }

        public class WorkflowStatus
        {
            public string LAST_ACTIVITY { get; set; }
            public string LAST_ACTION { get; set; }
            public string STATUS { get; set; }
        }

        public class Originator
        {
            public string DISPLAY_NAME { get; set; }
            public string EMAIL { get; set; }
        }

        public List<string> GetEmailNotification(int requestHeaderId, string requestCode, string activityCode, bool returnAsString = false)
        {
            var email = SqlQuery<string>("[REUSABLE].[EMAIL_NOTIFICATION] @requestCode=@requestCode, @ActivityCode=@ActivityCode, @requestHeaderId=@requestHeaderId "
                        , new SqlParameter("@requestCode", requestCode)
                        , new SqlParameter("@ActivityCode", activityCode)
                        , new SqlParameter("@requestHeaderId", requestHeaderId)                        
                        );

            return email.ToList();
        }

        public List<EmailAccount> GetEmailModification(int requestHeaderId)
        {
            var users = SqlQuery<EmailAccount>("[BPMDATA].[DESTINATION_USERS] @RequestHeaderId=" + requestHeaderId + 
                ", @ActivityName='Modification', @IsMergeActivity=1 ");

            return users.ToList();
        }

        public List<string> GetMTFEmailNotification(int requestHeaderId)
        {
            try
            {
                // participantBased:0 as submitter and requestor, 1 as submitter, 2 as Requestor
                // returnString:0 return string, 1 return list
                var email = SqlQuery<string>(@"EXEC BPMDATA.GET_EMAILS_BY_REQ 
                        @requestID = " + requestHeaderId + @",
	                    @requestCode = 'MT_REQ',
	                    @roleCode = 'FYI',
	                    @participantBased = 2,
	                    @returnString = 0,
	                    @seperator = ',',
	                    @package = 'BPMDATA.DEPT_APPROVAL_ROLE.ID.MTF.FYI'"
                    );
                return email.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<FormLongPending> GetFormLongPending(int maxDays = 0, string requestCode = "") {
            return SqlQuery<FormLongPending>("[SCHEDULER].[PROC_FORM_LONG_PENDING] @MAX_DAYS=@MaxDays, @REQUEST_CODE=@RequestCode", 
                new SqlParameter("@MaxDays", maxDays),
                new SqlParameter("@RequestCode", requestCode)
                ).ToList();
        }
    }

    
}
