Ext.define('Workflow.view.ticket.setting.item.ItemPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-item-item',
    data: {
        name: 'Workflow',
        query: null,
        status: 'ACTIVE',
        columnDateWidth: 135,
        columnStatusWidth: 75
    },
    stores: {
        ticketSettingItemStore: {
            model: 'Workflow.model.ticket.TicketSettingItem',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/getItems',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }
});
