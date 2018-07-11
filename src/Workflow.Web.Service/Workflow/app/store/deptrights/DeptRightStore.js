Ext.define('Workflow.store.deptrights.DeptRightStore', {
    extend: 'Ext.data.Store',
    model: 'Workflow.model.deptrights.Dept',
    alias: 'store.dept-rights-DeptRightStore',
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/deptrights/department',
        reader: {
            type: 'json',
            rootProperty: 'Records'
        }
    }
});
