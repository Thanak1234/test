Ext.define('Workflow.store.common.OOF', {
    extend: 'Ext.data.Store',
    alias: 'store.OOFs',
    model: 'Workflow.model.common.worklists.OOF',
    pageSize: 20,
    autoLoad: true    
});