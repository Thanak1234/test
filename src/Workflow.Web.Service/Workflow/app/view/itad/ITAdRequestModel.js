Ext.define('Workflow.view.itad.ITAdRequestModel', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/itadrequest'
    }
});
