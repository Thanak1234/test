Ext.define('Workflow.view.ticket.setting.sla.SlaPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-sla-sla',
    data: {
        name: 'Workflow',
        query: null,
        columnDateWidth: 135,
        columnStatusWidth: 75
    },
    stores: {
        ticketSettingSlaStore: {
            model: 'Workflow.model.ticket.TicketSla',
            autoLoad: false,            
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/sla/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    },
    formulas: {
        calcualteDate: function(get){
            console.log('get ', get);
        }
    }
});
