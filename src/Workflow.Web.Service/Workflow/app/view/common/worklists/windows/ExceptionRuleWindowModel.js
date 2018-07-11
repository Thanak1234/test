Ext.define('Workflow.view.common.worklist.ExceptionRuleWindowModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-worklist-exceptionrulewindow',
    data: {
        name: 'Workflow',
        selectedException: null,
        exceptionTemp: null,
        selectedDestination: null
    },   
    stores:{
        processStoreId: {
            model:'Workflow.model.common.worklists.Process',
            autoLoad: true
        },
        activityStoreId: {
            model:'Workflow.model.common.worklists.Activity',
            autoLoad: true
        }        
    }
});
