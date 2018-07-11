using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Workflow.DataAcess;
using Workflow.MSExchange;
using System.Data.SqlClient;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Scheduler;
using System.Data.Entity;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories;
using Workflow.ReportingService.Report;
using Workflow.MSExchange.Core;

namespace Workflow.Scheduler.Jobs {

    public abstract class ScheduleJobBase : IJob, IScheduleJob {

        protected IEmailService _EmailService;
        protected Job _Job;
        protected const int FROM = 0;
        protected const int TO = 0;
        protected const int CC = 1;
        protected const int BCC = 2;
        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected List<EmailFileAttachment> Attachments { get; set; }

        protected IDbFactory _DbFactory {
            get;
            private set;
        }

        protected IJobRepository _JobRepository;
        protected IEmailContentRepository _EmailContentRepository;
        protected IRecipientRepository _RecipientRepository;

        protected ScheduleJobBase() {
            _EmailService = new EmailService();
            _DbFactory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);

            _JobRepository = new JobRepository(_DbFactory);
            _EmailContentRepository = new EmailContentRepository(_DbFactory);
            _RecipientRepository = new RecipientRepository(_DbFactory);

            _Job = _JobRepository.Get(p => p.KeyValue == KeyValue);
            Attachments = new List<EmailFileAttachment>();
        }

        public void Execute(IJobExecutionContext context) {
            try {
                logger.Info(string.Format("{0} execute start.", context.FireInstanceId));
                ExecuteJob(context);
                logger.Info(string.Format("{0} execute done.", context.FireInstanceId));
            } catch(Exception ex) {
                logger.Error(ex.Message, ex);
            }
        }

        protected virtual void SendEmail() {
            if (_Job == null) throw new Exception("Job not found in database.");
            var emailContent = _EmailContentRepository.Get(p => p.JobId == _Job.Id);

            if (emailContent == null) throw new Exception("Email Content not found in database.");

            var toEmails = GetRecipients(emailContent.Id, TO);
            var ccEmails = GetRecipients(emailContent.Id, CC);
            var bccEmails = GetRecipients(emailContent.Id, BCC);

            _EmailService.SendEmail(emailContent.Subject, emailContent.MessageBody, toEmails, ccEmails, bccEmails, Attachments, Model);
        }

        protected virtual List<string> GetRecipients(int emailId, int type) {            
            return _RecipientRepository
                .GetMany(p => p.EmailId == emailId && p.Type == type)
                .Select(p => p.Email)
                .ToList();
        }

        public void Register(IScheduler scheduler) {
            var schedule = JobBuilder.Create(Type)
                .WithIdentity(_Job.Name)
                .StoreDurably()
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(Guid.NewGuid().ToString())
                .WithCronSchedule(_Job.CronExpression)                
                .Build();

            scheduler.ScheduleJob(schedule, trigger);
            if(_Job.Status == 0) {
                scheduler.PauseTrigger(trigger.Key);
            }
        }

        protected abstract void ExecuteJob(IJobExecutionContext context);
        protected abstract string KeyValue { get; }
        protected abstract dynamic Model { get; }
        protected abstract Type Type { get; }
        
    }
}
