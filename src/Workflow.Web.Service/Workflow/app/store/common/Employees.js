Ext.define('Workflow.store.common.Employees', {
    extend: 'Ext.data.Store',    
    alias: 'store.employees',         
    model: 'Workflow.model.common.Employee',
    pageSize: 20
    
});