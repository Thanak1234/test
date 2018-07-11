Ext.define('Workflow.view.ticket.setting.team.RegisteredAgentWindowModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-team-registeredagentwindowmodel',
    data: {
        name: 'workflow',
        form: {
            agent: null,
            immediateAssign: false    
        }
    },
    stores: {
        agentStore : {
            model: 'Workflow.model.ticket.TicketSettingAgent',
            autoLoad: true,            
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/agent/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    },
    formulas: {

    }
});