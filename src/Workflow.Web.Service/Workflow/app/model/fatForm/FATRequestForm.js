Ext.define('Workflow.model.fatForm.FATRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/fatrequest'
    }
});
