Ext.define('Workflow.view.itcr.ITCRRequestModel', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/itcrrequest'
    }
});
