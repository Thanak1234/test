Ext.define("Workflow.view.reports.itcr.ITCRReport", {
    xtype: 'report-itcr',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-itcr",
    viewModel: {
        type: "report-itcr"
    },
    report: {
        criteria: 'report-itcrCriteria',
        url: 'api/itcrreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
