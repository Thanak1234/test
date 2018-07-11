Ext.define('Workflow.view.reports.admsr.ADMSRReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-admsr',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
