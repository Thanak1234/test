Ext.define('Workflow.view.reports.rsvnff.RSVNFFReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-rsvnff',
    buildConfig: function (showField) {
        showField.AppName = false;
        return showField;
    },
    buildParam: function (criteria) {
        return criteria;
    },
    buildModel: function (fields) {
        return fields;
    }
});