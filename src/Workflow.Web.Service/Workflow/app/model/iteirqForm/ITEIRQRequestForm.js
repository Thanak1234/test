Ext.define('Workflow.model.iteirqForm.ITEIRQRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/iteirqrequest'
    }
});
