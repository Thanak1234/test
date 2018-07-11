Ext.define('Workflow.view.ApplicationComponentController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.application-component',
    actionUrl: null,
    renderSubForm: function (data, acl) {
        var me = this,
            view = me.getView(),
            references = view.getReferences();
        
        this.actionUrl = view.actionUrl;
        if (references) {
            
            for (var key in references) {
                var reference = references[key];
                reference.directive = data;
            }
            view.loadDataToView(references, data, acl);
        }
    },
    afterTakeAction: function (data) {
        var view = this.getView();
        if (view.afterTakeAction) {
            view.afterTakeAction(data);
        }
    },
    validateForm: function (data) {
        var me = this,
            view = me.getView(),
            references = view.getReferences();

        if(view.validateForm){
            return view.validateForm(references, data);
        }
    },
    confirmMessage: function (data) {
        var me = this,
            view = me.getView(),
            references = view.getReferences();

        if(view.confirmMessage){
            return view.confirmMessage(references, data);
        }
    },
    getRequestItem: function () {
        var me = this,
            view = me.getView(),
            reference = view.getReferences(),
            viewSetting = view.getWorkflowFormConfig();

        if (reference) {
            return view.bindPayloadData(reference);
        }

        return data;
    },
    clearData: function () {
        var me = this,
            view = me.getView(),
            references = view.getReferences();

        if(view.clearData){
            return view.clearData(references);
        }
    },
    isTextFieldBlank: function (text) {
        return text == null || Ext.isEmpty(text);
    }
});