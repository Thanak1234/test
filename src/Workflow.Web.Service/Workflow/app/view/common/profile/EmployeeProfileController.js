Ext.define('Workflow.view.common.profile.EmployeeProfileController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.common-profile-employeeprofilecontroller',
      
    sumbmitForm:function(){ 
        Ext.MessageBox.show({
            title: 'Confirm',
            msg: 'Are you sure you want to make change information?',
            buttons: Ext.MessageBox.YESNO,
            scope: this,
            fn: function(bt){
                if(bt==='yes') {
                    // after yes click the button
                    var rec = this.getViewModel().get('employeeprofile'); 
                    rec.store.sync(); 
                }
            }
        });
    }
     
});
