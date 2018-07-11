Ext.define('Workflow.view.ticket.setting.agent.AgentPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-agent-agent',
    data: {
        name: 'Workflow',
        query: null,
        columnDateWidth: 135,
        columnStatusWidth: 75
    },
    stores: {
        ticketSettingAgentStore: {
            model: 'Workflow.model.ticket.TicketSettingAgent',
            autoLoad: false,            
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/agent/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        employeeInfoStore: {
            model: 'Workflow.model.common.EmployeeInfo',
                autoLoad: false,
                proxy: {
                    type: 'rest',
                    url: Workflow.global.Config.baseUrl + 'api/employee/search',
                    reader: {
                        type: 'json',
                        rootProperty: 'data',
                        totalProperty: 'totalCount'
                    }
                }
            }
    }
});
