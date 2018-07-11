Ext.define('Workflow.view.hr.erf.RequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.erf-requestform',
    actionUrl: Workflow.global.Config.baseUrl + 'api/erfrequest',
    renderSubForm: function (data) {
        
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView,            
            viewSetting = view.getWorkflowFormConfig();        
        formView.fireEvent('loadData', { requisition: data && data.requisition ? data.requisition : null, viewSetting: viewSetting });        

    },
    validateForm: function () {
        var me = this,
           view = me.getView(),
           formView = view.getReferences().formView,
           model = formView.getViewModel();

        if (!formView.form.isValid()) {
            return "Some field(s) of form request is required. Please input the required field(s) before you click the Submit button.";
        }

    },
    getRequestItem : function(){
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView,
            formModel = formView.getViewModel(),
            ref = me.getReferences();
        var requisition = formModel.getData().requisition;
        var data = {
            requisition: requisition
        }
        return data;
    },
    clearData: function () {
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView;

        formView.getForm().reset();
    },
    isTextFieldBlank: function (text) {        
        return text == null || Ext.isEmpty(text);
    }
});
