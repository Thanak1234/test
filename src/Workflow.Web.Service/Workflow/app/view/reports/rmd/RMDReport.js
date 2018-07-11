Ext.define("Workflow.view.reports.rmd.RMDReport", {
    xtype: 'report-rmd',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-rmd",
    viewModel: {
        type: "report-rmd"
    },
    report: {
        criteria: 'report-rmdCriteria',
        url: 'api/rmdreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
