Ext.define('Workflow.model.vaf.VARequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/vafrequest'
    }
});
