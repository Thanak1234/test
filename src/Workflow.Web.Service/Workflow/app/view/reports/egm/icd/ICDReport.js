Ext.define("Workflow.view.reports.icd.ICDReport", {
    xtype: 'report-icd',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-icd",
    viewModel: {
        type: "report-icd"
    },
    report: {
        criteria: 'report-icdCriteria',
        url: 'api/icdreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
