Ext.define("Workflow.view.reports.tascr.TASCRReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-tascrCriteria',
    title: 'Fixed Asset Transfer - Criteria',
    viewModel: {
        type: "report-tascr"
    },
    buildFields: function (fields) {
        var me = this;

        return fields;
    }
});