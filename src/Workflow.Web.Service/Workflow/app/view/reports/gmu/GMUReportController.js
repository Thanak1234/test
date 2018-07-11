Ext.define('Workflow.view.reports.gmu.GMUReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-gmu',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
