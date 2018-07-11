Ext.define('Workflow.view.ticket.setting.grouppolicy.AssignTeamFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-grouppolicy-assignteamform',
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
            'status': null,
            'team': null
        }
    },
    formulas:{
        isEdit: function (get) {
            return true;
        }

    },
    stores: {
        ticketSettingTeamsStore: {
             model: 'Workflow.model.ticket.TicketTeam',
             autoLoad: false,
             proxy: {
                 type: 'rest',
                 url: Workflow.global.Config.baseUrl + 'api/ticket/setting/team/list',
                 reader: {
                     type: 'json',
                     rootProperty: 'data',
                     totalProperty: 'totalCount'
                 }
             }
        }
        // gridAssignedTeamStore: {
        //     model: 'Workflow.model.ticket.TicketGroupPolicyTeams',
        //     autoLoad: false,
        //     proxy: {
        //         type: 'rest',
        //         url: Workflow.global.Config.baseUrl + 'api/ticket/setting/grouppolicy/team-list',
        //         reader: {
        //             type: 'json',
        //             rootProperty: 'data',
        //             totalProperty: 'totalCount'
        //         }
        //     }
        // }
    }

});
