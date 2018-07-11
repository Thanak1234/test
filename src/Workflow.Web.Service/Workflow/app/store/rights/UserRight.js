Ext.define('Workflow.store.rights.UserRight', {
    extend: 'Ext.data.Store',
    model: 'Workflow.model.rights.UserRight',
    alias: 'store.rights-user',
    autoLoad: false,
    pageSize: 50,
    remoteSort: true,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/rights/users',
        reader: {
            type: 'json',
            rootProperty: 'Records',
            totalProperty: 'TotalRecords'
        },
        enablePaging: true
    }
});
