Ext.define('Workflow.model.mtfForm.MTFRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/mtfrequest'
    }
});
