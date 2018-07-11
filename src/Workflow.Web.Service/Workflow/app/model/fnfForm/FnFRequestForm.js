Ext.define('Workflow.model.fnfForm.FnFRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/fnfrequest'
    }
});
