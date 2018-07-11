Ext.define('Workflow.store.rights.Activity', {
    extend: 'Ext.data.Store',
    model: 'Workflow.model.rights.Activity',
    alias: 'store.rights-activities',
    autoLoad: true,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/rights/activities',
        reader: {
            type: 'json',
            rootProperty: 'Records'
        }
    }
});
