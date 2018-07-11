using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.MSExchange.Core;
using Workflow.ReportingService.Report;

namespace Workflow.Scheduler.Jobs
{

    public class CourseRegistrationJob : ScheduleJobBase, IScheduleJob {

        protected override string KeyValue {
            get {
                return "COURSE_REGISTRATION_JOB";
            }
        }

        protected override object Model {
            get {
                return null;
            }
        }

        protected override Type Type {
            get {
                return GetType();
            }
        }

        protected override void ExecuteJob(IJobExecutionContext context) {
            SendNotification();
        }

        private void SendNotification() {
            var repository = new Repository();
            var emails = repository.ExecDynamicSqlQuery(@"SELECT RH.* FROM HR.COURSE_REGISTRATION R 
                                                            INNER JOIN BPMDATA.REQUEST_HEADER RH 
                                                            ON R.RequestHeaderId = RH.ID
                                                            WHERE 
                                                            CAST(R.ReminderOn AS DATE) = CAST(GETDATE() AS DATE) AND 
                                                            RH.STATUS IN ('Approved', 'Edit')
                                                            AND CURRENT_ACTIVITY IN ('Email Notification', 'Modification')");

            var repo = new WMRepository(_DbFactory);
            var RequestApp = repo.GetReqAppByCode("TASCR_REQ");
            IRequestHeaderRepository requestHeaderRepository = new RequestHeaderRepository(_DbFactory);
            if (!IsNullEmpty(emails)) {
                foreach(var e in emails) {
                    List<EmailFileAttachment> attachments = new List<EmailFileAttachment>();
                    var recipients = new List<string>();
                    var originator = requestHeaderRepository.GetRequestorEmail(e.submittedBy);
                    recipients.Add(originator.EMAIL);
                    var ccs = repository.ExecDynamicSqlQuery(string.Format(@"[BPMDATA].[DESTINATION_USERS] @RequestHeaderId = {0}, @ActivityName = 'Email Notification', @IsMergeActivity = 1", e.id));
                    List<string> ccParticipants = new List<string>();
                    if (!IsNullEmpty(ccs)) {
                        foreach(var cc in ccs) {
                            ccParticipants.Add(cc.email);
                        }
                    }
                    var genericForm = new GenericFormRpt();
                    byte[] buffer = genericForm.Export(new GenericFormParam { RequestHeaderId = e.id }, RequestApp.ReportPath, ExportType.Pdf);
                    var FileName = string.Concat(e.title, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
                    var fileAttachments = new EmailFileAttachment(FileName, buffer);
                    attachments.Add(fileAttachments);
                    var emailContent = _EmailContentRepository.Get(p => p.JobId == _Job.Id);
                    string content = emailContent.MessageBody.Replace("@Originator", originator.DISPLAY_NAME).Replace("@Title", e.title);
                    _EmailService.SendEmail("Reminder Notification", content, recipients, ccParticipants, null, attachments, null);
                }
            }
        }

        private bool IsNullEmpty(IEnumerable<object> collection) {
            return collection == null || collection.Count() == 0;
        }
        
    }
}
