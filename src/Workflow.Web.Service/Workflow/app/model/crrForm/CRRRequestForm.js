Ext.define('Workflow.model.crrForm.CRRRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/crrrequest'
    }
});
