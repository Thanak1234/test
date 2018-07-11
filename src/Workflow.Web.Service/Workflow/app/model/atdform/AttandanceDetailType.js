Ext.define('Workflow.model.atdform.DetailType', {
    extend: 'Workflow.model.Base',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/atd/detailtype'
    }
});