Ext.define('Workflow.view.common.profile.EmployeeProfileWindowModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-profile-employeeprofilewindow',      
    formulas:{
        submitBtText : function(get) { 
            var textLabel = 'Submit', action = get('action');
            if(action==='ADD'){
                textLabel = 'Add';
            }else if (action==='EDIT'){
                textLabel = 'Edit';
            } 
            return textLabel;
        }, 
        submitBtVisible: function(get){
            var action = get('action');
            return action !=='VIEW'
            
        }
    }    

});
