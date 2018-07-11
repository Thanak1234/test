using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.DataAcess.Repositories;
using Workflow.Domain.Entities.RMD;

namespace Workflow.Scheduler.Jobs
{

    public class RmdNotification : ScheduleJobBase, IScheduleJob {

        protected object _model = null;

        protected override string KeyValue {
            get {
                return "RMD_NOTIFICATION_JOB";
            }
        }

        protected override dynamic Model {
            get {
                return _model;
            }
        }

        protected override Type Type {
            get {
                return GetType();
            }
        }

        protected void SendEmail(List<string> toEmails, List<string> ccEmails)
        {
            if (_Job == null) throw new Exception("Job not found in database.");
            var emailContent = _EmailContentRepository.Get(p => p.JobId == _Job.Id);

            if (emailContent == null) throw new Exception("Email Content not found in database.");
            
            var bccEmails = GetRecipients(emailContent.Id, BCC);

            _EmailService.SendEmail(emailContent.Subject, emailContent.MessageBody, toEmails, ccEmails, bccEmails, Attachments, Model);
        }
        
        protected override void ExecuteJob(IJobExecutionContext context) {
            var repository = new Repository();
            var reqs = repository.ExecSqlQuery<RMDModel>(
                @"SELECT RequestHeaderId, Title, OpenFormUrl, RequestorId, Requestor, EmailTo 
                  FROM [VIEW_RMD_COMPLETION_LIST] 
                  GROUP BY RequestHeaderId, Title, OpenFormUrl, RequestorId, Requestor, EmailTo");

            foreach (var req in reqs) {
                _model = req;
                var toEmails = new List<string>();
                toEmails.Add(req.EmailTo); // add submitor email

                var ccEmails = repository.ExecSqlQuery<string>(string.Format(
                    @"SELECT E.EMAIL ccEmail FROM BPMDATA.DEPT_APPROVAL_ROLE R 
                    INNER JOIN BPMDATA.USER_ROLE U ON U.ROLE_CODE = R.DEPT_APPROVAL_ROLE
                    INNER JOIN HR.EMPLOYEE E ON E.ID = U.EMP_ID
                    WHERE R.REQUEST_CODE = 'RMD_REQ' AND R.ROLE_CODE = 'HOD'
                    AND R.DEPT_ID IN (
	                    SELECT REQ.DEPT_ID FROM HR.EMPLOYEE REQ WHERE REQ.ID = {0}
                    )", req.RequestorId));
                SendEmail(toEmails, ccEmails.ToList());
                repository.ExecCommand(string.Format(@"UPDATE Worksheet2 SET [Notified] = 1 WHERE RequestHeaderId = {0}", req.RequestHeaderId));
            }            
        }
    }
    
    public class RMDModel {
        public int RequestHeaderId { get; set; }
        public int RequestorId { get; set; }
        public string Requestor { get; set; }
        public string EmailTo { get; set; }
        public string Title { get; set; }
        public string OpenFormUrl { get; set; }
    }
}
