/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using Workflow.Business.Ticketing.Impl;
using Workflow.Business.Ticketing.Dto;
using Workflow.Domain.Entities.Ticket;
using System.Text.RegularExpressions;
using Workflow.DataObject.Ticket;
using Workflow.DataObject.Worklists;
using Workflow.DataContract.K2;

namespace Workflow.Service.Ticketing
{
    public class K2ActionHandlers : Business.Ticketing.IActivityMessageHandler
    {
        private ITicketEnquiry dataEnquiry;
        private DataObject.EmployeeDto  loginUsr;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public K2ActionHandlers(ITicketEnquiry dataEnquiry, DataObject.EmployeeDto loginUsr )
        {
            this.dataEnquiry = dataEnquiry;
            this.loginUsr = loginUsr;
        }

        public void onActivityCreation(TicketActivity activity, AbstractTicketParam tkParam)
        {
            if (!ChangeStatusActivityHandler.ACTIVITY_CODE.IsCaseInsensitiveEqual(activity.ActivityType))
            {
                return;
            }
            var param = (ChangeStatusActParams)tkParam;

            if (param.StatusId == 5 || param.StatusId == 6)
            {
                var workListItem = dataEnquiry.GetWorkListItem(activity.TicketId, loginUsr.loginName);

                if (workListItem == null)
                {
                    return;
                }

                if (workListItem.requestCode == "IT_REQ")
                {
                    CloseITForm(workListItem, activity);
                }
                //else if (workListItem.requestCode == "GMU_REQ")
                //{
                //    CloseGmuRamForm(workListItem, activity);
                //}

                var actDto = new TicketActivityDataParser(new DataObject.Ticket.ActivityDto()
                {
                    ActivityCode = K2IntegratedActivityHander.ACTIVITY_CODE,
                    ActionCode = "Close Form",
                    Comment = string.Format("Form {0} is closed.", workListItem.Folio),
                    CurrUser = loginUsr,
                    TicketId = activity.TicketId
                });
                new TicketService().takeAction(actDto);
            }
        }

        private void CloseITForm(ProcInst workListItem, TicketActivity activity)
        {
            var formService = new ItRequestFormService();
            var itParam = new Domain.Entities.BatchData.ItRequestWorkflowInstance()
            {
                RequestHeaderId = workListItem.RequestHeaderId,
                Activity = "IT Implementation",
                Action = "Done",
                Comment = GetPlainTextFromHtml(activity.Description),
                SerialNo = workListItem.Serial,
                CurrentUser = loginUsr.loginName,
                loginName = loginUsr.loginName,
                fullName = loginUsr.fullName,
                Requestor = new Domain.Entities.Employee
                {
                    Id = loginUsr.id
                },
                AddUploadFiles = new List<Domain.Entities.FileAttachement>(),
                DelUploadFiles = new List<Domain.Entities.FileAttachement>(),
                UploadFiles = new List<Domain.Entities.FileAttachement>()
            };
            var errorMsg = string.Empty;
            try
            {
                formService.TakeAction(itParam);
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                logger.Error(e);
            }
        }

        private void CloseGmuRamForm(ProcInst workListItem, TicketActivity activity)
        {
            var formService = new GMURequestFormService();
            var gmuParam = new Domain.Entities.BatchData.GMURequestWorkflowInstance()
            {
                RequestHeaderId = workListItem.RequestHeaderId,
                Activity = "IT Configuration",
                Action = "Configured",
                Comment = GetPlainTextFromHtml(activity.Description),
                SerialNo = workListItem.Serial,
                CurrentUser = loginUsr.loginName,
                loginName = loginUsr.loginName,
                fullName = loginUsr.fullName,
                Requestor = new Domain.Entities.Employee
                {
                    Id = loginUsr.id
                },
                AddUploadFiles = new List<Domain.Entities.FileAttachement>(),
                DelUploadFiles = new List<Domain.Entities.FileAttachement>(),
                UploadFiles = new List<Domain.Entities.FileAttachement>()
            };
            var errorMsg = string.Empty;
            try
            {
                formService.TakeAction(gmuParam);
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                logger.Error(e);
            }
        }

        public void onTicketCreation(Ticket ticket)
        {
        }

        private string GetPlainTextFromHtml(string htmlString)
        {
            string htmlTagPattern = "<.*?>";
            var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            htmlString = regexCss.Replace(htmlString, string.Empty);
            htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
            htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            htmlString = htmlString.Replace("&nbsp;", string.Empty);

            return htmlString;
        }
    }
}
