Ext.define('Workflow.view.atd.AttandanceRequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.atd-attandancerequestform',
    actionUrl: Workflow.global.Config.baseUrl + 'api/atdrequest',
    renderSubForm: function (data) {
        //debugger;
        var me = this,
            view = me.getView(),                        
            attandanceInformation = view.getReferences().attandanceInformation,
            viewSetting = view.getWorkflowFormConfig();
        
        attandanceInformation.fireEvent('loadData', { Attandance: data && data.Attandance ? data.Attandance : null, viewSetting: viewSetting });
        
    },
    validateForm: function (data) {
        var me = this,
        view = me.getView(),                   
        attandanceInformation = view.getReferences().attandanceInformation,
        model = attandanceInformation.getViewModel();

        if (!attandanceInformation.form.isValid())
            return "Attandance Information field(s) is required. Please input the required field(s).";

            
    },
    getRequestItem: function () {
        //debugger;
        var me = this,
            view = me.getView(),
            reference = view.getReferences();
        
        var modelAttandance = view.getReferences().attandanceInformation.getViewModel().getData().Attandance;

        var data = {
            
            Attandance: modelAttandance
        };

        return data;
    },
    clearData: function () {
        var me = this,
            view = me.getView(),
            
            attandanceInformation = view.getReferences().attandanceInformation;

        
        attandanceInformation.getForm().reset();
        
    },
    isTextFieldBlank: function (text) {        
        return text == null || Ext.isEmpty(text);
    }
});
