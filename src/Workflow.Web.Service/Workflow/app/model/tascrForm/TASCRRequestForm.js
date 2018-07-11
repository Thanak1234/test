Ext.define('Workflow.model.tascrForm.TASCRRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/tascrrequest'
    }
});
