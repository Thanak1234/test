Ext.define('Workflow.view.reports.bcj.BCJReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-bcj',
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
        capexCategories: {
            model: 'Workflow.model.bcjForm.CapexCategory',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/bcjrequest/capexcategories'
            }
        }
    }
});