Ext.define('Workflow.view.reports.rac.RACReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-rac',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
