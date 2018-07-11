Ext.define('Workflow.store.deptrights.DeptAccessStore', {
    extend: 'Ext.data.Store',
    model: 'Workflow.model.deptrights.DeptAccessList',
    alias: 'store.dept-rights-deptaccessstore',
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/deptrights/departmentaccess',
        reader: {
            type: 'json',
            rootProperty: 'records',            
            totalProperty: 'count'
        }
    }
});
