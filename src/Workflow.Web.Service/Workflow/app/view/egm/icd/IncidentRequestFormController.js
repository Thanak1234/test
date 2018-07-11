Ext.define('Workflow.view.icd.IncidentRequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.icd-incidentrequestform',
    actionUrl: Workflow.global.Config.baseUrl + 'api/icdrequest',
    renderSubForm: function (data) {
        //debugger;
        var me = this,
            view = me.getView(),            
            employeeView = view.getReferences().employeeList,
            incidentoutlineView = view.getReferences().incidentOutline,
            incidentInformation = view.getReferences().incidentInformation,
            viewSetting = view.getWorkflowFormConfig();

        incidentoutlineView.fireEvent('loadData', { Incident: data && data.Incident ? data.Incident : null, viewSetting: viewSetting });
        incidentInformation.fireEvent('loadData', { Incident: data && data.Incident ? data.Incident : null, viewSetting: viewSetting });
        employeeView.fireEvent('loadData', { data: data && data.IncidentEmployeeList ? data.IncidentEmployeeList : null, viewSetting: viewSetting });
    },
    validateForm: function (data) {
        var me = this,
        view = me.getView(),
        employeeView = view.getReferences().employeeList,
        incidentoutline = view.getReferences().incidentOutline,
        incidentInformation = view.getReferences().incidentInformation,
        model = incidentoutline.getViewModel();

        if (employeeView.getStore().getCount() < 1)
            return "Staff List is required. Please add at least one Staff!";

        if(!incidentInformation.form.isValid())
            return "Incident Information field(s) is required. Please input the required field(s).";

        if (!incidentoutline.form.isValid()) {
            return "Incident Outline field(s) is required. Please input the required field(s).";
        }        
    },
    getRequestItem: function () {
        //debugger;
        var me = this,
            view = me.getView(),
            reference = view.getReferences();
        
        var modelLocation = view.getReferences().incidentInformation.getViewModel().getData().Incident;
        var modelOutline = view.getReferences().incidentOutline.getViewModel().getData().Incident;

        modelOutline.mcid = modelLocation.mcid;
        modelOutline.gamename = modelLocation.gamename;
        modelOutline.zone = modelLocation.zone;
        modelOutline.customername = modelLocation.customername;
        modelOutline.cctv = modelLocation.cctv;

        var data = {
            IncidentEmployeeList: me.getOriginDataFromCollection(reference.employeeList.getStore().getNewRecords()),
            AddIncidentEmployee: me.getOriginDataFromCollection(reference.employeeList.getStore().getNewRecords()),
            DelIncidentEmployee: me.getOriginDataFromCollection(reference.employeeList.getStore().getRemovedRecords()),
            EditIncidentEmployee: me.getOriginDataFromCollection(reference.employeeList.getStore().getUpdatedRecords()),
            Incident: modelOutline
        };

        return data;
    },
    clearData: function () {
        var me = this,
            view = me.getView(),
            employeeList = view.getReferences().employeeList,
            incidentInformation = view.getReferences().incidentInformation,
            incidentOutline = view.getReferences().incidentOutline
            ;

        employeeList.getStore().removeAll();
        incidentInformation.getForm().reset();
        incidentOutline.getForm().reset();
    },
    isTextFieldBlank: function (text) {        
        return text == null || Ext.isEmpty(text);
    }
});
