/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using Workflow.Business.Ticketing.Impl;
using Workflow.Business.Ticketing.Impl.DataFill;
using Workflow.DataObject;
using Workflow.DataObject.Notification;
using Workflow.DataObject.Ticket;

namespace Workflow.Business.Ticketing
{
    public class FillMoreActData : IFillMoreActData
    {
        private IDataProcessingProvider dataPsProvider;
        private List<string> actvityTypesFlag = new List<string>();
        private List<object> actvityTypes = new List<object>();

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region

        public FillMoreActData(IDataProcessingProvider dataPsProvider)
        {
            this.dataPsProvider = dataPsProvider;
        }

        public void fill(SimpleActivityViewDto act)
        {
            fill(null, act);
        }

        public void fill(TicketViewDto ticket, SimpleActivityViewDto act)
        {
            
            //if (ticket != null && TicketActivityHandler.ACTIVITY_CODE.Equals(act.activityType))
            //{
            //    act.subject = ticket.subject;
            //}
            //else 

            if (!actvityTypesFlag.Exists(t => t.Equals(act.activityType)))
            {
                actvityTypesFlag.Add(act.activityType);
                actvityTypes.Add(new { id= act.activityType, display = act.activityName });
            }


            if (ticket != null)
            {
                ticket.activityTypes = actvityTypes;
            }

            if (AssignTicketActivityHandler.ACTIVITY_CODE.Equals(act.activityType))
            {

                try
                {
                    var assignedInfo = dataPsProvider.getAssingeeInfo(act.id);

                    act.team = assignedInfo.team;
                    act.assignee = assignedInfo.assignee;
                    act.empNoAssignee = assignedInfo.empNoAssignee;
                    act.assignedExpired = assignedInfo.expired;
                    if (act.actionBy == null)
                    {
                        act.actionBy = "System";
                    }
                }catch(Exception e)
                {
                    logger.Error(e);
                }
                

            }
            else if (ChangeStatusActivityHandler.ACTIVITY_CODE.Equals(act.activityType))
            {
                act.moreData = dataPsProvider.getChangeStatusDesc(act.id);
            }

            else if (TicketMergedActivityHandler.ACTIVITY_CODE.Equals(act.activityType))
            {
                act.addData = dataPsProvider.getMergeInfo(act.id);
            }
            else if(SubTicketActivityHandler.ACTIVITY_CODE.Equals(act.activityType))
            {
                act.addData = dataPsProvider.getSubTitket(act.id);
            }
            else if (TicketActivityHandler.ACTIVITY_CODE.Equals(act.activityType))
            {

                act.addData = dataPsProvider.getMainTicket(act.id);
                if (act.addData == null)
                {
                    var k2integrate = dataPsProvider.getFormIntegrated(ticket.id);
                    act.addData = k2integrate;
                    if(k2integrate != null)
                    {
                        ticket.description = dataPsProvider.GetITFormContent(k2integrate.Id);
                    }                    
                }

            }

            act.fileUpload = getFileInfo(act);
            if (ticket != null)
            {
                if (ticket.fileUpload == null)
                {
                    ticket.fileUpload = new List<FileUploadInfo>();
                }

                ticket.fileUpload.AddRange(act.fileUpload);

                if (!ticket.hasAttachment && act.fileUpload.Count > 0)
                {
                    ticket.hasAttachment = true;
                }
            }

            if(ticket != null && ticket.reference != null)
            {
                var email = dataPsProvider.GetEmailItem(ticket.reference);
                if (email != null)
                {
                    ticket.emailItem = email;
                }
            }
        }


        private List<FileUploadInfo> getFileInfo(SimpleActivityViewDto act)
        {
            List<FileUploadInfo> fileInfos = new List<FileUploadInfo>();

            var files = dataPsProvider.getFileUpload(act.id);

            files.ForEach(t =>
            {
                fileInfos.Add(new FileUploadInfo()
                {
                    fileName = t.FileName,
                    activityName = act.activityName,
                    ext = t.Ext,
                    serial = t.UploadSerial,
                    createdDate = t.CreatedDate
                });
            });

            return fileInfos;
        }


        #endregion

        #region
        public NotifyDataDto fill(TKNotifyDto notifyData)
        {

            var metaData = getMetaData(notifyData);
            var desVal = metaData.GetType().GetProperty("description").GetValue(metaData);

            var description = string.Empty;
            if (desVal != null)
            {
                description = (string)desVal;
            }

            return new NotifyDataDto()
            {
                Id = notifyData.NotifyId,
                Model = "Ticket",
                ActivityId = notifyData.ActivityId,
                ActivityCode = notifyData.ActivityType,
                Status = notifyData.Status,
                Subject = notifyData.ActivityName,
                MetaData = metaData,
                Description = description,
                CreatedDate = notifyData.CreatedDate
            };
        }

        private object getMetaData(TKNotifyDto notifyData)
        {
            IActivityDataFill activityDataFill = null;
            if (TicketActivityHandler.ACTIVITY_CODE.Equals(notifyData.ActivityType) || SubTicketActivityHandler.ACTIVITY_CODE.Equals(notifyData.ActivityType))
            {
                activityDataFill = new TicketCreationActivityDataFill(dataPsProvider);
            }else if (AssignTicketActivityHandler.ACTIVITY_CODE.Equals(notifyData.ActivityType))
            {
                activityDataFill = new TicketAssignmentActivityDataFill(dataPsProvider);
            }else if (PostReplyActivityHandler.ACTIVITY_CODE.Equals(notifyData.ActivityType))
            {
                activityDataFill = new PostReplyActivityDataFill(dataPsProvider);
            }else if (ChangeStatusActivityHandler.ACTIVITY_CODE.Equals(notifyData.ActivityType))
            {
                activityDataFill = new TicketStatusActivityDataFill(dataPsProvider);
            }

            if (activityDataFill!=null)
            {
                return activityDataFill.getData(notifyData.ActivityId);
            }else
            {
                throw new Exception("Activity data fill implement found.");
            }
            
           
        }

        /**
        Fill None register employee
        */
        public void fill(TicketViewDto ticket)
        {
           if(ticket.empId >0)
            {
                return;
            }

            try
            {
                var info = dataPsProvider.getUnknowRequestor(new Domain.Entities.Ticket.Ticket() { Id = ticket.id });

                ticket.empName = info.DisplayName;
                ticket.empNo = info.EmpNo;
                ticket.email = info.Email;
            }
            catch
            {
                return;
            }
            

        }

        #endregion
    }
}
