Ext.define("Workflow.view.reports.mcn.MCNReport", {
    xtype: 'report-mcn',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-mcn",
    viewModel: {
        type: "report-mcn"
    },
    report: {
        criteria: 'report-mcnCriteria',
        url: 'api/mcnreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
