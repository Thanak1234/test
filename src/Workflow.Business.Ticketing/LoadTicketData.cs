/**
*@author : Phanny
*/
using System.Collections.Generic;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;

namespace Workflow.Business.Ticketing
{
    public class LoadTicketData : ILoadTicketData
    {

        private IDataProcessingProvider dataProcessingProvider;

        public LoadTicketData(IDataProcessingProvider dataProcessingProvider)
        {
            this.dataProcessingProvider = dataProcessingProvider;
        }

       

        public TicketViewDto getTicketView(int id,EmployeeDto emp)
        {
            var ticketViewDto = dataProcessingProvider.getTicketView(id);
            var activities = dataProcessingProvider.getActivity(id, emp);
            //var activitesView = new List<AbstractActivityViewDto>();
            ticketViewDto.activities = activities;

            //var agent = dataProcessingProvider.getAgentByEmpId(emp.id);

            //if (agent.Id > 0 && ticketViewDto.assigneeId != emp.id)
            //{
            //    var canAccessAsAgent = dataProcessingProvider.canAccessAsAgent(agent.Id, (int)ticketViewDto.teamId);
            //    if (!canAccessAsAgent )
            //    {
            //        agent = null;
            //    }
            //}


            ticketViewDto.actions = getActions(id, emp, ticketViewDto);//dataProcessingProvider.getAvailableActions(ticketViewDto.stateId, agent==null?0: agent.Id);
            IFillMoreActData fillMoreActData = new FillMoreActData(dataProcessingProvider);
            fillMoreActData.fill(ticketViewDto);
            foreach (var act in activities)
            {
                fillMoreActData.fill(ticketViewDto, act);

            }


            if (ticketViewDto.hasAttachment)
            {
                ticketViewDto.actions.Add(new ActionDto() {
                    activityCode ="VIEW_ATTACHMENT",
                    name ="All Attachments",
                    groupName = "LAST"
                });
            }

            return ticketViewDto;
        }


        public List<ActionDto> getActions(int id, EmployeeDto emp, TicketViewDto ticketViewDto = null )
        {
            if(ticketViewDto == null)
            {
                ticketViewDto = dataProcessingProvider.getTicketView(id);
            }
            
            var agent = dataProcessingProvider.getAgentByEmpId(emp.id);


            if (agent !=null && agent.Id > 0 && ticketViewDto.assignedEmpId != emp.id)
            {
                var canAccessAsAgent = dataProcessingProvider.canAccessAsAgent(agent.Id, (int)ticketViewDto.teamId);
                if (!canAccessAsAgent)
                {
                    agent = null;
                }
            }


            var actions = dataProcessingProvider.getAvailableActions(ticketViewDto.stateId, agent == null ? 0 : agent.Id);
            return actions;
        }

        private List<ActionDto> getAvailableActions()
        {
            return null;
        }

        
    }
}
