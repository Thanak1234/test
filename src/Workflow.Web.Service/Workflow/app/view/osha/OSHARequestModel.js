Ext.define('Workflow.view.osha.OSHARequestModel', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/osharequest'
    }
});
