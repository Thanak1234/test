Ext.define('Workflow.view.icd.employeeList.requestEmployeeWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.icd-employeelist-requestemployeewindow',
    
    parseItem: function (){
        var me = this,
            record = me.getView().getViewModel().parseToUserRequestItem();
        return record;
          
    }
});
