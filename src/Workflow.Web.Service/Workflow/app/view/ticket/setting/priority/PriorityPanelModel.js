Ext.define('Workflow.view.ticket.setting.priority.PriorityPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-priority-priority',
    data: {
        name: 'Workflow',
        query: null,
        columnDateWidth: 135,
        columnStatusWidth: 75
    },
    stores: {
        ticketSettingPriorityStore: {
            model: 'Workflow.model.ticket.TicketPriority',
            autoLoad: false,            
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/priority/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }
});
