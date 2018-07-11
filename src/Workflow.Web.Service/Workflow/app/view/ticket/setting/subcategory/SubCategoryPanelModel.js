Ext.define('Workflow.view.ticket.setting.subcategory.SubCategoryPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-subcategory-subcategory',
    data: {
        name: 'Workflow',
        query: null,
        status: 'ACTIVE',
        columnDateWidth: 135,
        columnStatusWidth: 75
    },
    stores: {
        ticketSettingSubCategoryStore: {
            model: 'Workflow.model.ticket.TicketSettingSubCategory',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/subcate/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }
});
