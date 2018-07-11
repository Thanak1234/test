Ext.define('Workflow.view.ticket.setting.team.TicketTeamFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-team-ticketteamform',
    data: {                        
        form: {
            'id': null,
            'teamName': null,
            'alertAllMembers': null,            
            'alertAssignedAgent': null,
            'directoryListing': null,
            'description': null,
            'createdDate': null,
            'modifiedDate': null,
            'status': null
        },        
        isAgentSelected: false,
        selectedAgentRow: null,
        btnAssignImmediateName: 'Assign Immediate'
    },
    formulas:{        
        isEdit: function (get) {
            return  get('id')!=null;
        }

    },
    stores: {        
        statusStore: {
            model: 'Workflow.model.ticket.TicketLookup'
        },
        ticketSettingTeamAgentsStore: {
             model: 'Workflow.model.ticket.TicketSettingAgent',
             autoLoad: false,            
             proxy: {
                 type: 'rest',
                 url: Workflow.global.Config.baseUrl + 'api/ticket/setting/team/agent-list',
                 reader: {
                     type: 'json',
                     rootProperty: 'data',
                     totalProperty: 'totalCount'
                 }
             }
        }
    }

});
