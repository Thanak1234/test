Ext.define('Workflow.view.rac.RACRequestModel', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/racrequest'
    }
});
