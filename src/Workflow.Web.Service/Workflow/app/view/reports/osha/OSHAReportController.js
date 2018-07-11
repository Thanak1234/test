Ext.define('Workflow.view.reports.osha.OSHAReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-osha',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
