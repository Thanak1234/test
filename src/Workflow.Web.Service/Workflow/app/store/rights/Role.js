Ext.define('Workflow.store.rights.Role', {
    extend: 'Ext.data.Store',
    model: 'Workflow.model.rights.Role',
    alias: 'store.rights-role',
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/rights/roles'
    }
});
