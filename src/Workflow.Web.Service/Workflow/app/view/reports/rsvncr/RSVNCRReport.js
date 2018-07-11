Ext.define("Workflow.view.reports.rsvncr.RSVNCRReport", {
    xtype: 'report-rsvncr',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-rsvncr",
    viewModel: {
        type: "report-rsvncr"
    },
    report: {
        criteria: 'report-rsvncrCriteria',
        url: 'api/rsvncrprocessinstant'
    },
    buildColumns: function (columns) {
        return columns;
    }
});
