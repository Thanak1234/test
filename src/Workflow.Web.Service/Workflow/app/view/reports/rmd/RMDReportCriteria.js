Ext.define("Workflow.view.reports.rmd.RMDReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-rmdCriteria',
    //title: 'RMD - Criteria',
    reqCode: 'RMD_REQ',
    viewModel: {
        type: "report-rmd"
    },
    buildFields: function (fields) {
        var me = this;

        return fields;
    }
});