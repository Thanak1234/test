Ext.define('Workflow.view.wm.WorklistManagementModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.worklistmanagement',
    data: {
        name: 'workflow'
    },
    stores: {
        forms: {
            model: 'Workflow.model.common.worklists.Process',
            autoLoad: true
        },
        activities: {
            model: 'Workflow.model.common.worklists.Activity',
            autoLoad: true
        }
    }    
});
