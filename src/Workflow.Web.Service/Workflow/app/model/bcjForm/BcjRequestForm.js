Ext.define('Workflow.model.bcjForm.BcjRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/bcjrequest'
    }
});
