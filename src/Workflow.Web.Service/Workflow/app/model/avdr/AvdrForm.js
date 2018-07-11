Ext.define('Workflow.model.avdr.AvdrForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/AvdrForm'
    }
});
