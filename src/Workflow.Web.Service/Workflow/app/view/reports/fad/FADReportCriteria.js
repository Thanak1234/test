Ext.define("Workflow.view.reports.fad.FADReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-fadCriteria',
    title: 'Asset Disposal - Criteria',
    viewModel: {
        type: "report-fad"
    },
    buildFields: function (fields) {
        var me = this;

        return fields;
    }
});