Ext.define('Workflow.view.admsr.AdmsrRequestModel', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/admsrrequest'
    }
});
