Ext.define('Workflow.view.reports.av.AVReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-av',
    buildConfig: function (showField) {
        showField.AppName = false;
        return showField;
    },
    buildParam: function (criteria) {
        criteria.ItemType = null;
        return criteria;
    },
    buildModel: function (fields) {
        fields = [
             { name: 'itemTypeName', type: 'string' }
        ]
        return fields;
    }
});