Ext.define('Workflow.view.reports.rsvncr.RSVNCRReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-rsvncr',
    buildStore: function (store, params, viewmodel) {
        
        return store;
    }
});
