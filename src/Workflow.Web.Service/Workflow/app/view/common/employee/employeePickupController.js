Ext.define('Workflow.view.common.employee.employeePickupController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-employee-employeepickup',
    
    //onEmplyeePickupChanged : function( el, newValue, oldValue, eOpts){
    //    console.log('onchange');
    //    if(newValue){
    //        el.getTrigger('clear').show();
    //        // this.setViewData(el.getSelection());
            

    //    }else{
    //        el.getTrigger('clear').hide();
    //        // this.getView().getViewModel().set('employeeInfo', null);            
    //    }  
          
    //},

    onEmplyeePickupChanged: function (queryPlan, eOpts) {
        var me = this;
        var v = me.getView();

        var integrated = false,
            excludeOwner = false,
            includeInactive = false,
            includeGenericAcct = false;

        var store = v.getStore();

        if (v.integrated) {
            integrated = v.integrated;
        }

        if (v.excludeOwner) {
            excludeOwner = v.excludeOwner;
        }

        if (v.includeInactive) {
            includeInactive = v.includeInactive;
        }

        if (v.includeGenericAcct) {
            includeGenericAcct = v.includeGenericAcct;
        }

        if (store) {
            
            Ext.apply(store.getProxy().extraParams, {
                integrated: integrated,
                excludeOwner: excludeOwner,
                includeInactive: includeInactive,
                includeGenericAcct: includeGenericAcct,
                EmpId: 0
            });
        }

        if (!queryPlan.query) {        
            //if query is empty, try to get from text box
            queryPlan.query = v.rawValue;
        }

    }
    
});
