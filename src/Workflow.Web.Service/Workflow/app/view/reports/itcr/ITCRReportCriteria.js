Ext.define("Workflow.view.reports.itcr.ITCRReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-itcrCriteria',
    title: 'IT Change Request - Criteria',
    viewModel: {
        type: "report-itcr"
    },
    buildFields: function (fields) {
        var me = this;

        return fields;
    }
});