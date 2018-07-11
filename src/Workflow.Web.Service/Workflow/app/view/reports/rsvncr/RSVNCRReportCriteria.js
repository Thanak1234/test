Ext.define("Workflow.view.reports.rsvncr.RSVNCRReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-rsvncrCriteria',
    title: 'Complimentary Room Request - Criteria',
    viewModel: {
        type: "report-rsvncr"
    },
    buildFields: function (fields) {
        return fields;
    }
});