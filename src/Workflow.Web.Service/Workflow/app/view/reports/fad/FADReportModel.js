Ext.define('Workflow.view.reports.fad.FADReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-fad',
    buildConfig: function (showField) {
        showField.AppName = false;
        return showField;
    },
    buildParam: function (criteria) { 
        return criteria;
    },
    buildModel: function (fields) {
        fields = [
             
        ]
        return fields;
    }
});