Ext.define('Workflow.view.reports.va.VAReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-va',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
