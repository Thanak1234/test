Ext.define('Workflow.view.reports.avir.AvirReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-avir',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
