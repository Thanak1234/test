/* global store */
/* global employeeInfo */
Ext.define('Workflow.view.common.profile.EmployeeProfileViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.employeeprofile',
    data: {
         name: 'Workflow' 
    },  
    
    
    stores: {
        profileStoreId: {
            /*model: 'Workflow.model.profile.EmployeeProfileModel',*/
            type:'employeeprofile'
            /*autoLoad: true,
            autoSync: true,
            session: true,
            listeners: {
                load: function() {

                },
                update: function() {
                    Ext.Msg.alert('Message', 'Customer updated!');
                }
            }*/
        }
    },
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
            
        },
        
        employeeprofile: {
            get: function(get) { 
                return get('profileStoreId').first();
            }
        }
    }    

});
