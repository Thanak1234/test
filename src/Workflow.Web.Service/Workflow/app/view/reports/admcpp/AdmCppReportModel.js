Ext.define('Workflow.view.reports.admcpp.AdmCppReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-admcpp',
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