/**
*@author : Phanny
*/
using HandlebarsDotNet;
using System;
using Workflow.Business.Ticketing.Dto;
using Workflow.Business.Ticketing.Impl;
using Workflow.DataObject.Ticket;

namespace Workflow.Service.Ticketing
{
    public class TicketActivityDataParser : ITicketDataParser
    {

        private ActivityDto dto;

        public TicketActivityDataParser(ActivityDto dto)
        {
            this.dto = dto;
        }

        public AbstractTicketParam parse()
        {
            
            var template = Handlebars.Compile(dto.Comment);
            dto.Comment = template(new { });

            if (AssignTicketActivityHandler.ACTIVITY_CODE.Equals(dto.ActivityCode))
            {
                return assignTicket();

            }
            else if (PostReplyActivityHandler.ACTIVITY_CODE.Equals(dto.ActivityCode))
            {
                return postReplyTicket();

            }
            else if (PostInternalNoteActivityHandler.ACTIVITY_CODE.Equals(dto.ActivityCode) 
                || PostPublicNoteActivityHandler.ACTIVITY_CODE.Equals(dto.ActivityCode) 
                || RemoveTicketActivityHandler.ACTIVITY_CODE.Equals(dto.ActivityCode)
                || K2IntegratedActivityHander.ACTIVITY_CODE.Equals(dto.ActivityCode))
            {
                return simpleActivityParam();
            }
           
            else if (ChangeStatusActivityHandler.ACTIVITY_CODE.Equals(dto.ActivityCode))
            {
                return changeStatusParam();
            }else if (TicketMergedActivityHandler.ACTIVITY_CODE.Equals(dto.ActivityCode))
            {
                return mergeTicketParam();
            }

            throw new Exception(String.Format("{0} is not supported.", dto.ActivityCode));
        }


        private AbstractTicketParam postReplyTicket()
        {
            return new PostReplyTicketParams()
            {
                TicketId = dto.TicketId,
                ActivityCode = dto.ActivityCode,
                ActionCode = dto.ActionCode,
                ActComment = dto.Comment,
                CurrLoginUserId = dto.CurrUser.id,
                FileUploads = dto.FileUploads
                
            };
        }


        private AbstractTicketParam assignTicket()
        {
            return new AssignedTicketParams()
            {
                TicketId = dto.TicketId,
                ActivityCode = dto.ActivityCode,
                ActionCode = dto.ActionCode,
                ActComment = dto.Comment,
                TeamId = dto.TeamId,
                Assignee = dto.Assignee,
                FileUploads = dto.FileUploads,
                CurrLoginUserId = dto.CurrUser.id,

            };
        }


        private AbstractTicketParam simpleActivityParam()
        {
            return new SimpleActParams()
            {
                TicketId = dto.TicketId,
                ActivityCode = dto.ActivityCode,
                ActionCode = dto.ActionCode,
                ActComment = dto.Comment,
                CurrLoginUserId = dto.CurrUser.id,
                FileUploads = dto.FileUploads
            };
        }

        private AbstractTicketParam changeStatusParam()
        {
            return new ChangeStatusActParams()
            {
                TicketId = dto.TicketId,
                ActivityCode = dto.ActivityCode,
                ActionCode = dto.ActionCode,
                ActComment = dto.Comment,
                CurrLoginUserId = dto.CurrUser.id,
                FileUploads = dto.FileUploads,
                ActualMinutes = dto.ActualMinutes,
                StatusId = dto.StatusId,
                closeK2Form = dto.CloseK2Form,
                RootCauseId = dto.RootCauseId
            };
        }


        private AbstractTicketParam mergeTicketParam()
        {
            return new MergedTicketActParam()
            {
                TicketId = dto.TicketId,
                ActivityCode = dto.ActivityCode,
                ActionCode = dto.ActionCode,
                ActComment = dto.Comment,
                CurrLoginUserId = dto.CurrUser.id,
                FileUploads = dto.FileUploads,
                MergedToTkId = dto.MergedToTkId
            };
        }

        public ActivityDto getActivityDto()
        {
            return dto;
        }
    }
}
