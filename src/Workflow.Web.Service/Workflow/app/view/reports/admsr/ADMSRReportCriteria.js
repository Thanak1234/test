Ext.define("Workflow.view.reports.admsr.ADMSRReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-admsrcriteria',
    title: 'Admin Service Request - Criteria',
    viewModel: {
        type: "report-admsr"
    },
    buildFields: function (fields) {
        var me = this;

        return fields;
    }
});