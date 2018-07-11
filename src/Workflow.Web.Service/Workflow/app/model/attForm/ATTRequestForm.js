Ext.define('Workflow.model.attForm.ATTRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/attrequest'
    }
});
