Ext.define('Workflow.view.ticket.setting.sla.SlaMappingPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-sla-slamappingpanelmodel',
    data: {
        
    },
    stores: {
        ticketSettingSlaMappingStore : {
            model: 'Workflow.model.ticket.TicketSettingSlaMappingModel',
            autoLoad: true,            
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/sla/mapping/list',
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