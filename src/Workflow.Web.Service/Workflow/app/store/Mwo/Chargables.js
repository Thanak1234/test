Ext.define('Workflow.store.Chargables', {
    extend: 'Ext.data.Store',
    alias: 'store.mwo-chargables',
    storeId: 'mwo-chargables',    
    model: 'Workflow.model.mwo.Chargable',
    autoLoad: true,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/mwoitem/departmentChargables'
    }
});
