Ext.define('Workflow.model.avir.AvirForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/AvirForm'
    }
});
