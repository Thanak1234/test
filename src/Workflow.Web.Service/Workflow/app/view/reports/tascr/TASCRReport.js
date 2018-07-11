Ext.define("Workflow.view.reports.tascr.TASCRReport", {
    xtype: 'report-tascr',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-tascr",
    viewModel: {
        type: "report-tascr"
    },
    report: {
        criteria: 'report-tascrCriteria',
        url: 'api/tascrreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
