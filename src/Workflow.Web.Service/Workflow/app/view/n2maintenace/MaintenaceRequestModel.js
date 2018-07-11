Ext.define('Workflow.view.n2maintenace.MaintenaceRequestModel', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/n2mworequest'
    }
});
