Ext.define('Workflow.view.reports.mwo.MWOReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-mwo',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
