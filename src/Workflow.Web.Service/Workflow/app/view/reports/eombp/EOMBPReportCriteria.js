Ext.define("Workflow.view.reports.eombp.EOMBPReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-eombpCriteria',
    title: 'Fixed Asset Transfer - Criteria',
    viewModel: {
        type: "report-eombp"
    },
    buildFields: function (fields) {
        var me = this;

        return fields;
    }
});