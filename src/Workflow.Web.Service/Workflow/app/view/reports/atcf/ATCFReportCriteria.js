Ext.define("Workflow.view.reports.atcf.ATCFReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-atcfCriteria',
    //title: 'Additional Time Worked Claim - Criteria',
    viewModel: {
        type: "report-atcf"
    },
    buildFields: function (fields) {
        var me = this;

        return fields;
    }
});