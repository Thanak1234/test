Ext.define('Workflow.view.ticket.setting.grouppolicy.GroupPolicyPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-grouppolicy-grouppolicy',
    data: {
        name: 'Workflow',
        query: null,
        columnDateWidth: 135,
        columnStatusWidth: 75
    },
    stores: {
        ticketSettingGroupPolicyStore: {
            model: 'Workflow.model.ticket.TicketGroupPolicy',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/grouppolicy/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        assignedTeamStore: {
             model: 'Workflow.model.ticket.TicketGroupPolicyTeams',
             autoLoad: false,
             proxy: {
                 type: 'rest',
                 url: Workflow.global.Config.baseUrl + 'api/ticket/setting/grouppolicy/team-list',
                 reader: {
                     type: 'json',
                     rootProperty: 'data',
                     totalProperty: 'totalCount'
                 }
             }
        },
        assignedReportAccessTeamStore: {
             model: 'Workflow.model.ticket.TicketGroupPolicyTeams',
             autoLoad: false,
             proxy: {
                 type: 'rest',
                 url: Workflow.global.Config.baseUrl + 'api/ticket/setting/grouppolicy/team-list',
                 reader: {
                     type: 'json',
                     rootProperty: 'data',
                     totalProperty: 'totalCount'
                 }
             }
        }
    }
});
