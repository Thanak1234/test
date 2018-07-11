Ext.define('Workflow.view.reports.av.AVReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-av',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
