Ext.define("Workflow.view.reports.ibr.IBRReport", {
    xtype: 'report-ibr',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-ibr",
    viewModel: {
        type: "report-ibr"
    },
    report: {
        criteria: 'report-ibrCriteria',
        url: 'api/ibrreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
