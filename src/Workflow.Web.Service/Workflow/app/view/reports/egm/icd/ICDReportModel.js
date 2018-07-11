Ext.define('Workflow.view.reports.icd.ICDReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-icd',
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