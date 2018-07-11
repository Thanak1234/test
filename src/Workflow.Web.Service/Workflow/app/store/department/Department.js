Ext.define('Workflow.store.department.Department', {
    extend: 'Ext.data.Store',
    
    alias: 'store.department',
         
    model: 'Workflow.model.common.Department',

    pageSize: 20,

    autoLoad: false,

    proxy: {
        //type: 'ajax',
        //url: '~api/requestor',
        type: 'rest',        
        url: Workflow.global.Config.baseUrl + 'api/employee/searchdept',
        //callbackKey: 'callback1',
        reader: {
            type: 'json',
            rootProperty: 'data',
            totalProperty: 'totalCount'
        }
    }
});
