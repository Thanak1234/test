Ext.define('Workflow.store.rights.RoleRight', {
    extend: 'Ext.data.Store',
    model: 'Workflow.model.rights.RoleRight',
    alias: 'store.rights-roleright',
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/rights/rolerights'        
    }
});
