Ext.define('Workflow.view.reports.jram.JRAMReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-jram',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
