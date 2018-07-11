/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.AssignedActivityModel', {
    extend:'Workflow.view.ticket.activity.ActivityModel',
    alias: 'viewmodel.ticket-activity-assignedactivity',
    data: {
        team: null,
        agent: null,
        requiredComment : false
    },
    stores: {
        ticketTeamStore: {
            model: 'Workflow.model.ticket.TicketTeam',
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/team-list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        ticketAgentStore: {
            model: 'Workflow.model.common.GeneralLookup',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/agent-list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }

        }
    }

});
