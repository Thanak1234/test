Ext.define('Workflow.model.mcnform.MachineType', {
    extend: 'Workflow.model.Base',
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/mcn/issuetype'
    }
});