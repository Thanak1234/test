Ext.define("Workflow.view.reports.att.ATTReport", {
    xtype: 'report-att',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-att",
    viewModel: {
        type: "report-att"
    },
    report: {
        criteria: 'report-attCriteria',
        url: 'api/attreport'
    },
    buildColumns: function (columns) {
        var me = this;

        return columns;
    }
});
