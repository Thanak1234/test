using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Scheduler;
using Workflow.Scheduler.Cores;
using Workflow.Service;
using Workflow.Service.Interfaces;

namespace Workflow.Web.Service.Controllers.Scheduler
{
    [RoutePrefix("api/scheduler")]
    public class SchedulerController : ApiController
    {
        protected ISchedulerService _SchedulerService;
        protected ISchedulerCommand _SchedulerCommand;

        public SchedulerController() {
            _SchedulerService = new SchedulerService();
            _SchedulerCommand = new SchedulerCommand(SchedulerProvider.Instance);
        }
        // GET: api/Scheduler
        [Route("data")]
        public SchedulerDataOutput Get()
        {
            return _SchedulerCommand.GetSchedulerData();
        }

        [HttpPost]
        [Route("startScheduler")]
        public void StartScheduler()
        {
            _SchedulerCommand.StartScheduler();
        }

        [HttpPost]
        [Route("stopScheduler")]
        public void StopScheduler() {
            _SchedulerCommand.StopScheduler();
        }

        [HttpPost]
        [Route("reschedule")]
        public void Reschedule(InputParameter param) {
            _SchedulerCommand.Reschedule(param);
            _SchedulerService.UpdateCronExpression(param.Job, param.CronExpression);
        }

        [HttpPost]
        [Route("ExecuteAction")]
        public void ExecuteAction(InputParameter param) {
            switch(param.Method) {
                case "PauseGroup":
                    _SchedulerCommand.PauseGroup(param);
                    break;
                case "PauseJob":
                    _SchedulerCommand.PauseJob(param);
                    break;
                case "PauseTrigger":
                    _SchedulerCommand.PauseTrigger(param);
                    _SchedulerService.UpdateStatus(param.Job, 0);
                    break;
                case "ResumeJob":
                    _SchedulerCommand.ResumeJob(param);
                    break;
                case "ResumeGroup":
                    _SchedulerCommand.ResumeGroup(param);
                    break;
                case "ResumeTrigger":
                    _SchedulerCommand.ResumeTrigger(param);
                    _SchedulerService.UpdateStatus(param.Job, 1);
                    break;
                case "ExecuteNow":
                    _SchedulerCommand.ExecuteNow(param);
                    break;
                case "DeleteGroup":
                    _SchedulerCommand.DeleteGroup(param);
                    break;
                case "DeleteJob":
                    _SchedulerCommand.DeleteJob(param);
                    break;
                case "DeleteTrigger":
                    _SchedulerCommand.DeleteTrigger(param);
                    break;
            }
        }
    }
}
