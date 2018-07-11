Ext.define('Workflow.view.reports.mt.MTReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-mt',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
