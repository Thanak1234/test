Ext.define('Workflow.model.mwo.MwoRequestForm', {
    extend: 'Workflow.model.common.RequestForm',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/mworequest'
    }
});
