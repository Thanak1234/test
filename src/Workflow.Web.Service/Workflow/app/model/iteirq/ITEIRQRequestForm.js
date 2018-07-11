Ext.define('Workflow.model.fadForm.ITEIRQRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/iteirqrequest'
    }
});
