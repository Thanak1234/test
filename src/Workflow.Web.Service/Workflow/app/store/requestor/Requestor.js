Ext.define('Workflow.store.requestor.Requestor', {
    extend: 'Ext.data.Store',
    
    alias: 'store.requestor',
         
    model: 'Workflow.model.common.EmployeeInfo',

    pageSize: 20,

    autoLoad: false,


    proxy: { 
        /*type: 'ajax',
        url: '~api/requestor',
        reader: {
            type: 'json',
            rootProperty: 'data'
        }*/
    	type: 'rest',        
    	url: Workflow.global.Config.baseUrl + 'api/employee/search',
        //callbackKey: 'callback1',
        reader: {
            type: 'json',
            rootProperty: 'data',
            totalProperty: 'totalCount'
        }
    }
});
