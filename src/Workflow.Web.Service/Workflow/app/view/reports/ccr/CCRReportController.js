Ext.define('Workflow.view.reports.ccr.CCRReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-ccr',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
