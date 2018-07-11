Ext.define('Workflow.model.atcfForm.ATCFRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/atcfrequest'
    }
});
