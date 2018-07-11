Ext.define("Workflow.view.reports.rsvnff.RSVNFFReport", {
    xtype: 'report-rsvnff',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-rsvnff",
    viewModel: {
        type: "report-rsvnff"
    },
    report: {
        criteria: 'report-rsvnffCriteria',
        url: 'api/rsvnffprocessinstant'
    },
    buildColumns: function (columns) {
        return columns;
    }
});
