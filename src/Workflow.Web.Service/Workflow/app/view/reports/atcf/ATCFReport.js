Ext.define("Workflow.view.reports.atcf.ATCFReport", {
    xtype: 'report-atcf',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-atcf",
    viewModel: {
        type: "report-atcf"
    },
    report: {
        criteria: 'report-atcfCriteria',
        url: 'api/atcfreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
