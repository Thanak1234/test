Ext.define('Workflow.model.eom.EOMRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/eomrequest'
    }
});
