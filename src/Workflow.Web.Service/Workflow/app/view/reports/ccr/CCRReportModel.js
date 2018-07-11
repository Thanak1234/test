Ext.define('Workflow.view.reports.ccr.CCRReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-ccr',
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