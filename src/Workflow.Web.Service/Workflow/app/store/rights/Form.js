Ext.define('Workflow.store.rights.Form', {
    extend: 'Ext.data.Store',
    model: 'Workflow.model.rights.Form',
    alias: 'store.rights-forms',
    autoLoad: true,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/rights/forms',
        reader: {
            type: 'json',
            rootProperty: 'Records'
        }
    }
});
