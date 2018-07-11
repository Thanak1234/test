Ext.define('Workflow.view.osha.EmployeeWinController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.osha-employeeWin',
    
    parseItem: function (){
        var me = this,
            record = me.getView().getViewModel().parseToUserRequestItem();
        return record;
          
    }
});
