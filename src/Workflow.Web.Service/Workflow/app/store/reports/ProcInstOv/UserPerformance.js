Ext.define('Workflow.store.reports.ProcInstOv.UserPerformance', {
    extend: 'Ext.data.Store',
    alias: 'store.userPerformance',
    model: 'Workflow.model.reports.ProcInstOv.UserPerformance',
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/procInstOv/performances',
        reader: {
            type: 'json',
            rootProperty: 'Records',
            totalProperty: 'TotalRecords'
        }
    }
});