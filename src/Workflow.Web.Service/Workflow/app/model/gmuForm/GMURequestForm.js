Ext.define('Workflow.model.gmuForm.GMURequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/gmurequest'
    }
});
