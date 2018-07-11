Ext.define("Workflow.view.reports.rsvnff.RSVNFFReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-rsvnffCriteria',
    title: 'Friend & Family Booking Request - Criteria',
    viewModel: {
        type: "report-rsvnff"
    },
    buildFields: function (fields) {
        return fields;
    }
});