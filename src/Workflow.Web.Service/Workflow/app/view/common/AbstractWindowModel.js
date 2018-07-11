/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.common.AbstractWindowModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-abstract-window-model',
    data: {
         action: 'ADD',
         item : null 
    },
    formulas:{
        submitBtText : function(get) { 
            var textLabel = 'Submit', action = get('action');
            if(action.toUpperCase()==='ADD'){
                textLabel = 'Add';
            }else if (action.toUpperCase()==='EDIT'){
                textLabel = 'Update';
            } 
            return textLabel;
        }, 
        submitBtVisible: function(get){
            var action = get('action');
            return action.toUpperCase() !=='VIEW'   
        },
        readOnlyField : function(get){
            return  (get('action')==='VIEW' || (get('item') &&   get('item').get('id')>0 ));
        },
        btnCloseIconCls: function (get) {
            return 'fa fa-times-circle-o';
        }
    }    

});