Ext.define("Workflow.view.reports.hgvr.HGVRReport", {
    xtype: 'report-hgvr',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-hgvr",
    viewModel: {
        type: "report-hgvr"
    },
    report: {
        criteria: 'report-hgvrcriteria',
        url: 'api/hgvrreport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;
        return columns;
    }
});


