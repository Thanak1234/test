Ext.define("Workflow.view.reports.ccr.CCRReport", {
    xtype: 'report-ccr',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-ccr",
    viewModel: {
        type: "report-ccr"
    },
    report: {
        criteria: 'report-ccrcriteria',
        url: 'api/ccrreport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;

        return columns;
    }
});


