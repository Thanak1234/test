Ext.define('Workflow.store.wm.Items', {
    extend: 'Ext.data.Store',
    alias: 'store.wm.items',
    model: 'Workflow.model.wm.Item',
    autoLoad: true,
    pageSize: 10,
    remoteSort : true,
    proxy: {
        type: 'rest',
        reader: {
            type: 'json',
            rootProperty: 'Records',
            totalProperty: 'TotalRecords'
        },
        enablePaging: true
    }
});