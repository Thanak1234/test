Ext.define('Workflow.store.roles.TreeItems', {
    extend: 'Ext.data.TreeStore',
    alias: 'store.roles-treeitems',
    autoLoad: false,
    storeId: 'RoleTreeItems',
    proxy: {
        type: 'ajax',
        url: Workflow.global.Config.baseUrl + 'api/roles/treeitems'
    }
});
