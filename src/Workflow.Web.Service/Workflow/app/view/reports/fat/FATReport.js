Ext.define("Workflow.view.reports.fat.FATReport", {
    xtype: 'report-fat',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-fat",
    viewModel: {
        type: "report-fat"
    },
    report: {
        criteria: 'report-fatCriteria',
        url: 'api/fatreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
