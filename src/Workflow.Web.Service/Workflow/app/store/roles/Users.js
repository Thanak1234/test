Ext.define('Workflow.store.roles.Users', {
    extend: 'Ext.data.Store',
    model: 'Workflow.model.roles.User',
    alias: 'store.roles-users',
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/roles/users',
        reader: {
            type: 'json',
            rootProperty: 'Records',
            totalProperty: 'TotalRecords'
        }
    }
});
