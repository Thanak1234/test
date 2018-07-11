Ext.define('Workflow.store.roles.Roles', {
    extend: 'Ext.data.Store',
    model: 'Workflow.model.roles.Role',
    alias: 'store.roles-roles',
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/roles/byUser',
        reader: {
            type: 'json',
            rootProperty: 'Records',
            totalProperty: 'TotalRecords'
        }
    }
});
