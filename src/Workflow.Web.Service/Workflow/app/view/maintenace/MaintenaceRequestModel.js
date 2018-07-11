Ext.define('Workflow.view.maintenace.MaintenaceRequestModel', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/mworequest'
    }
});
