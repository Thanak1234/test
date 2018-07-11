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
    public class TicketDataParser : ITicketDataParser
    {
        private TicketDto dto;
        public TicketDataParser(TicketDto ticketDto)
        {
            this.dto = ticketDto;
        }

        public ActivityDto getActivityDto()
        {
            return new ActivityDto {
                CurrUser = dto.CurrUser,
                ActionCode = dto.ActionCode,
                ActivityCode = dto.ActivityCode,
                TicketId = dto.TicketId
            };
        }

        public AbstractTicketParam parse()
        {
            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                var template = Handlebars.Compile(dto.Description);
                var desc = template(new { });
                dto.Description = desc;
            }
            if (!string.IsNullOrWhiteSpace(dto.Comment))
            {
                var template = Handlebars.Compile(dto.Comment);
                var comment = template(new { });
                dto.Comment = comment;
            }


            var param = new TicketParams()
            {
                TicketId = dto.TicketId,
                ActivityCode = dto.ActivityCode,
                ActionCode = dto.ActionCode,
                Subject = dto.Subject,
                Description = dto.Description,

                TeamId = dto.TeamId,
                Assignee = dto.Assignee,
                StatusId = dto.StatusId,
                SiteId = dto.SiteId,
                ImpactId = dto.ImpactId,
                DeptOwnerId = dto.DeptOwnerId,
                CurrLoginUserId = dto.CurrUser.id,
                PriorityId = dto.PriorityId,
                RequestorId = dto.RequestorId,
                SourceId = dto.SourceId,
                TicketItemId = dto.TicketItemId,
                TicketTypeId = dto.TicketTypeId,
                UrgencyId = dto.UrgencyId,
                UserAttachFiles = dto.UserAttachFiles,
                UserAttachFilesDel = dto.UserAttachFilesDel,
                ActComment = dto.Comment,
                EstimatedHours = dto.EstimatedHours,
                DueDate = dto.DueDate,
                TicketNoneReqEmp = dto.TicketNoneReqEmp,
                AutomationType = dto.IsAutomation? TicketParams.INTEGRATED_TYPE.EMAIL: TicketParams.INTEGRATED_TYPE.K2,
                SlaId = dto.SlaId,
                RootCause = dto.RootCause,
                RefType = dto.RefType,
                Reference = dto.Reference                
            };

            if (String.IsNullOrEmpty(param.ActivityCode))
            {
                param.ActivityCode = TicketActivityHandler.ACTIVITY_CODE;
            }
            return param;
        }
    }
}
