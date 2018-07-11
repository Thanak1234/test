Ext.define('Workflow.model.fadForm.FADRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/fadrequest'
    }
});
