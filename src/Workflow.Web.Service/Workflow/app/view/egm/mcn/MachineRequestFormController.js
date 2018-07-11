Ext.define('Workflow.view.mcn.MachineRequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.mcn-machinerequestform',
    actionUrl: Workflow.global.Config.baseUrl + 'api/mcnrequest',
    renderSubForm: function (data) {
        //debugger;
        var me = this,
            view = me.getView(),            
            employeeView = view.getReferences().employeeList,
            
            machineInformation = view.getReferences().machineInformation,
            viewSetting = view.getWorkflowFormConfig();

        //incidentoutlineView.fireEvent('loadData', { Incident: data && data.Incident ? data.Incident : null, viewSetting: viewSetting });
        machineInformation.fireEvent('loadData', { Machine: data && data.Machine ? data.Machine : null, viewSetting: viewSetting });
        employeeView.fireEvent('loadData', { data: data && data.MachineEmployeeList ? data.MachineEmployeeList : null, viewSetting: viewSetting });
    },
    validateForm: function (data) {
        var me = this,
        view = me.getView(),
        employeeView = view.getReferences().employeeList,
        machineInformation = view.getReferences().machineInformation,
        model = machineInformation.getViewModel();

        if (employeeView.getStore().getCount() < 1)
            return "Staff List is required. Please add at least one Staff!";

        if (!machineInformation.form.isValid())
            return "Machine Information field(s) is required. Please input the required field(s).";            
    },
    getRequestItem: function () {
        //debugger;
        var me = this,
            view = me.getView(),
            reference = view.getReferences();
        
        var modelMachine = view.getReferences().machineInformation.getViewModel().getData().Machine;        

        var data = {
            MachineEmployeeList: me.getOriginDataFromCollection(reference.employeeList.getStore().getNewRecords()),
            AddMachineEmployee: me.getOriginDataFromCollection(reference.employeeList.getStore().getNewRecords()),
            DelMachineEmployee: me.getOriginDataFromCollection(reference.employeeList.getStore().getRemovedRecords()),
            EditMachineEmployee: me.getOriginDataFromCollection(reference.employeeList.getStore().getUpdatedRecords()),
            Machine: modelMachine
        };

        return data;
    },
    clearData: function () {
        var me = this,
            view = me.getView(),
            employeeList = view.getReferences().employeeList,
            machineInformation = view.getReferences().machineInformation;

        employeeList.getStore().removeAll();
        machineInformation.getForm().reset();
        
    },
    isTextFieldBlank: function (text) {        
        return text == null || Ext.isEmpty(text);
    }
});
