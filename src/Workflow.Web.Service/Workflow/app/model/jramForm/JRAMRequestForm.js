Ext.define('Workflow.model.jramForm.JRAMRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/jramrequest'
    }
});
