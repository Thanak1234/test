Ext.define('Workflow.model.erfForm.ERFRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/erfrequest'
    }
});
