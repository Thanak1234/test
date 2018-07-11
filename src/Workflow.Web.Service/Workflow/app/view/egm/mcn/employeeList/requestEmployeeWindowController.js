Ext.define('Workflow.view.mcn.employeeList.requestEmployeeWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.mcn-employeelist-requestemployeewindow',
    
    parseItem: function (){
        var me = this,
            record = me.getView().getViewModel().parseToUserRequestItem();
        return record;
          
    }
});
