Ext.define('Workflow.view.reports.procInstOverview.ProcessInstanceOverviewModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-processinstanceoverview',
    buildConfig: function (showField) {
        showField.AppName = true;
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