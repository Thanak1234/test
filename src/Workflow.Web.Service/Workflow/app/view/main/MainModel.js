/**
 * This class is the view model for the Main view of the application.
 */

/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.main.MainModel', {
    extend: 'Ext.app.ViewModel',

    alias: 'viewmodel.main',

    data: {
        name: 'Workflow',
        workflowFormOpen: null,
        identity: null,
        notifiedCount: ''
        
    },
    
    stores:{
        uploadFileStoreId:{ 
            model:'Workflow.model.common.FileUpload',
            autoLoad: true 
        }
    },

    formulas: {
        identity: function (get) {
            return Workflow.global.UserAccount.identity;
        },
        notifyBtHide : function(get){
            var notifiedCount = get('notifiedCount');
            if(notifiedCount){
                return false;
            }else{
                return true;
            }
        }
    }
});
