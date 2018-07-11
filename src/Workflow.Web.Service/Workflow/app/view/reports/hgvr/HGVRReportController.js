Ext.define('Workflow.view.reports.hgvr.HGVRReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-hgvr',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
