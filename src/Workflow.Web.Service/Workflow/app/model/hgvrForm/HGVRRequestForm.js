Ext.define('Workflow.model.hgvrForm.HGVRRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/hgvrrequest'
    }
});
