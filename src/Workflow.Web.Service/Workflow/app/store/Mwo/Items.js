Ext.define('Workflow.store.mwo.Items', {
    extend: 'Ext.data.Store',
    alias: 'store.mwo-items',
    storeId: 'mwo-items',
    model: 'Workflow.model.mwo.Item',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/mwoitem/items',
        reader: {
            type: 'json',
            rootProperty: 'Records',
            totalProperty: 'TotalRecords'
        }
    }
});
