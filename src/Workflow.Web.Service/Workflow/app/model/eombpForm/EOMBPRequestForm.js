Ext.define('Workflow.model.eombpForm.EOMBPRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/eombprequest'
    }
});
