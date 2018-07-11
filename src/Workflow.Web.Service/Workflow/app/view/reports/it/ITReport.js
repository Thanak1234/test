Ext.define("Workflow.view.reports.it.ITReport", {
    xtype: 'report-it',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-it",
    viewModel: {
        type: "report-it"
    },
    report: {
        criteria: 'report-itCriteria',
        url: 'api/itreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
