Ext.define('Workflow.view.reports.itirf.ITIRFReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-itirf',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
