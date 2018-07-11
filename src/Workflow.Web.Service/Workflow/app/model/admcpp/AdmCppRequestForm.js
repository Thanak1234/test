Ext.define('Workflow.model.admcpp.AdmCppRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/admcpprequest'
    }
});
