Ext.define('Workflow.store.reports.ProcInstOv.ActivityInstance', {
    extend: 'Ext.data.Store',
    alias: 'store.procInstAI',
    model: 'Workflow.model.reports.ProcInstOv.ActivityInstance',
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/procInstOv/activities',
        reader: {
            type: 'json',
            rootProperty: 'Records',
            totalProperty: 'TotalRecords'
        }
    }
});