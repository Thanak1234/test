Ext.define('Workflow.model.itapp.ItAppRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/itapprequest'
    }
});
