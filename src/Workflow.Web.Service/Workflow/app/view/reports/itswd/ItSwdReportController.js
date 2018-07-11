Ext.define('Workflow.view.reports.itswd.ItSwdReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-itswd',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
