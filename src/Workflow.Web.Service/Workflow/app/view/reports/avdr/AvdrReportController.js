Ext.define('Workflow.view.reports.avdr.AvdrReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-avdr',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
