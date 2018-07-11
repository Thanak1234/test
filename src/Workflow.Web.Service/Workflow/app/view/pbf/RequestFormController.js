Ext.define('Workflow.view.pbf.RequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.pbf-requestform',
    actionUrl: Workflow.global.Config.baseUrl + 'api/pbfrequest',
    renderSubForm: function (data) {
        
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView,
            viewSetting = view.getWorkflowFormConfig(),
            specificationView = formView.getReferences().specificationView;

        formView.fireEvent('loadData', { projectBrief: data && data.projectBrief ? data.projectBrief : null, viewSetting: viewSetting });
        specificationView.fireEvent('loadData', { data: data && data.specifications ? data.specifications : null, viewSetting: viewSetting });

    },
    validateForm: function (data) {
        var me = this,
           view = me.getView(),
           formView = view.getReferences().formView,
           model = formView.getViewModel(),
           specificationView = formView.getReferences().specificationView;

        var briefing = model.get('projectBrief.briefing')
        if (data.activity == 'MARCOM Technical Briefing' && 
            (briefing == null || briefing == '') &&
            data.action == 'Submitted') {
            return "The (Technical Briefing) field of form request is required. Please input the required field before you click the Submit button.";
        }
        if (!formView.form.isValid()) {
            return "Some field(s) of form request is required. Please input the required field(s) before you click the Submit button.";
        } else if (specificationView.getStore().getCount() == 0) {
            return "There is no specificity item in this request. Please add at least one item before you click the Submit button.";
        }

    },
    getRequestItem : function(){
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView,
            formModel = formView.getViewModel(),
            ref = me.getReferences();

        var projectBrief = formModel.getData().projectBrief,
            specificationView = formView.getReferences().specificationView,
            specificationStore = specificationView.getStore();

        var data = {
            projectBrief: projectBrief,
            // Request Item
            addRequestItems: me.getOriginDataFromCollection(specificationStore.getNewRecords()),
            editRequestItems: me.getOriginDataFromCollection(specificationStore.getUpdatedRecords()),
            delRequestItems: me.getOriginDataFromCollection(specificationStore.getRemovedRecords())
        }

        return data;
    },  
    clearData: function () {
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView,
            specificationView = formView.getReferences().specificationView;

        formView.getForm().reset();
        specificationView.fireEvent('onDataClear');
    },
    isTextFieldBlank: function (text) {        
        return text == null || Ext.isEmpty(text);
    }
});
