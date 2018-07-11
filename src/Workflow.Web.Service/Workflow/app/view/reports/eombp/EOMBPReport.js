Ext.define("Workflow.view.reports.eombp.EOMBPReport", {
    xtype: 'report-eombp',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-eombp",
    viewModel: {
        type: "report-eombp"
    },
    report: {
        criteria: 'report-eombpCriteria',
        url: 'api/eombpreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
