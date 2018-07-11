Ext.define('Workflow.view.reports.eom.EomReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-eom',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
