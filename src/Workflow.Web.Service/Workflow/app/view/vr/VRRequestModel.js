Ext.define('Workflow.view.vr.VRRequestModel', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/vrrequest'
    }
});
