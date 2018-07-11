Ext.define('Workflow.view.common.department.DepartmentPickupController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-department-departmentpickup',
    
    onDeptPickupChanged : function( el, newValue, oldValue, eOpts){
       
        if(newValue){
            el.getTrigger('clear').show();
            // this.setViewData(el.getSelection());
        }else{
            el.getTrigger('clear').hide();
            // this.getView().getViewModel().set('employeeInfo', null);
        }  
          
    }
    
});
