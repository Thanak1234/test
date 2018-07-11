Ext.define("Workflow.view.reports.fad.FADReport", {
    xtype: 'report-fad',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-fad",
    viewModel: {
        type: "report-fad"
    },
    report: {
        criteria: 'report-fadCriteria',
        url: 'api/fadreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
