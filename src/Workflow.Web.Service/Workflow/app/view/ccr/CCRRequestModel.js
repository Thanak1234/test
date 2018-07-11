Ext.define('Workflow.view.ccr.CCRRequestModel', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/ccrrequest'
    }
});
