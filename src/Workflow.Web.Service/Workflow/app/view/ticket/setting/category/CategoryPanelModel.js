Ext.define('Workflow.view.ticket.setting.category.CategoryPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-category-category',
    data: {
        name: 'Workflow',
        query: null,
        status: 'ACTIVE',
        columnDateWidth: 135,
        columnStatusWidth: 75
    },
    stores: {
        ticketSettingCategoryStore: {
            model: 'Workflow.model.ticket.TicketSettingCategory',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/cate/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }
});
