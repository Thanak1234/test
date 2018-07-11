Ext.define("Workflow.view.reports.ibr.IBRReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-ibrCriteria',
    viewModel: {
        type: "report-ibr"
    },
    buildFields: function (fields) {
        var me = this;

        return fields;
    }
});