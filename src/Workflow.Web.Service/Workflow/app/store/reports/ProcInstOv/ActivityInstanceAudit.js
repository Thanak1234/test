Ext.define('Workflow.store.reports.ProcInstOv.ActivityInstanceAudit', {
    extend: 'Ext.data.Store',
    alias: 'store.procInstA',
    model: 'Workflow.model.reports.ProcInstOv.ActivityInstanceAudit',
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/procInstOv/audits',
        reader: {
            type: 'json',
            rootProperty: 'Records',
            totalProperty: 'TotalRecords'
        }
    }
});