Ext.define('Workflow.model.pbfForm.PBFRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/pbfrequest'
    }
});
