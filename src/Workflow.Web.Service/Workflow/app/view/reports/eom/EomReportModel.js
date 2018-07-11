Ext.define('Workflow.view.reports.eom.EomReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-eom',
    buildConfig: function (showField) {
        showField.AppName = false;
        return showField;
    },
    buildParam: function (criteria) {
        criteria.IncidentDate = null;
        criteria.ReporterId = null;

        return criteria;
    },
    buildModel: function (fields) {
        fields = [
             
        ]
        return fields;
    }
});