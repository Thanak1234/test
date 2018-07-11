/**
 * 
 *Author : Phanny 
 */
Ext.define('Workflow.view.common.requestor.AddRequestorModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-requestor-addrequestor',
    data: {
       action: 'EDIT',
       mainView: null, 
       employeeInfo : null
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
        }
    },
    submitBtVisible: function(get){
        var action = get('action');
        return action !=='View'
        
    }    

});
