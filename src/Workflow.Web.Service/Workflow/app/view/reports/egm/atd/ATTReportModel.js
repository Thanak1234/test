Ext.define('Workflow.view.reports.att.ATTReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-att',
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
    },
    stores: {
        types: {
            type: 'store',
            autoLoad: true,
            model: 'Workflow.model.atdform.DetailType'
        }
    }
});