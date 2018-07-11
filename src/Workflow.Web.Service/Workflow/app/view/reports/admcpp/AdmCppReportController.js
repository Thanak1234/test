Ext.define('Workflow.view.reports.admcpp.AdmCppReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-admcpp',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
