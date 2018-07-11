Ext.define('Workflow.store.deptrights.Form', {
    extend: 'Ext.data.Store',
    model: 'Workflow.model.deptrights.Form',
    alias: 'store.dept-rights-forms',
    autoLoad: true,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/deptrights/forms',
        reader: {
            type: 'json',
            rootProperty: 'Records'
        }
    }
});
