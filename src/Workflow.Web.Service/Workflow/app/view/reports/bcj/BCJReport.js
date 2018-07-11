Ext.define("Workflow.view.reports.bcj.BCJReport", {
    xtype: 'report-bcj',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-bcj",
    viewModel: {
        type: "report-bcj"
    },
    report: {
        criteria: 'report-bcjCriteria',
        url: 'api/bcjreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
