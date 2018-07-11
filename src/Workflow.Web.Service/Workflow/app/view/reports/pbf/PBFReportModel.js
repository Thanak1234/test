Ext.define('Workflow.view.reports.pbf.PBFReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-pbf',
    buildConfig: function (showField) {
        showField.AppName = false;
        return showField;
    },
    buildParam: function (criteria) { 
        return criteria;
    },
    buildModel: function (fields) {
        fields = [
             { name: 'projectName', type: 'string' },
             { name: 'projectReference', type: 'string' }
        ]
        return fields;
    }
});