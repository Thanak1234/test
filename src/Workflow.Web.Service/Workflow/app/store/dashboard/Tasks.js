Ext.define('Workflow.store.dashboard.Tasks', {
    extend: 'Ext.data.Store',
    alias: 'store.tasks',
    model: 'Workflow.model.dashboard.Task',
    autoLoad: false,
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