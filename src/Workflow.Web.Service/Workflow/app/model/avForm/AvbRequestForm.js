Ext.define('Workflow.model.avForm.AvbRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/avbrequest'
    }
});
