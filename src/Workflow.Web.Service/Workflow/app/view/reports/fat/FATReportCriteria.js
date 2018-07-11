Ext.define("Workflow.view.reports.fat.FATReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-fatCriteria',
    title: 'Fixed Asset Transfer - Criteria',
    viewModel: {
        type: "report-fat"
    },
    buildFields: function (fields) {
        var me = this;

        return fields;
    }
});