/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.bcj.RequestItemViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.bcj-requestitemview',
    data: {
        selectedItem: null,
        viewSetting: null,
        totalAmount: 0
    },
    stores : {
        dataStore : {
            model: 'Workflow.model.bcjForm.RequestItem'
        }
    },
    formulas: {
         canAddRemove : function(get){
            
            if(get('viewSetting') &&  get('viewSetting').requestItemBlock.addEdit){
                return true
            }else{
                return false;
            }
        },
        
        editable : function(get) {
            if (get('selectedItem') && get('viewSetting') && get('viewSetting').requestItemBlock.addEdit) {
                return true;
            } else {
                return false;
            }
            
        },
        disabled: function (get) {
            if (get('selectedItem') && get('viewSetting') && get('viewSetting').requestItemBlock.addEdit) {
                return false;
            } else {
                return true;
            }
        },
        
        canView : function(get) {
            if(get('selectedItem')){
                return true;
            }else{
                return false;
            }
            
        },
        canAdd : function(get){
            if(get('sessionSelection')){
                return true;
            }else{
                return false;
            }
        }
    }

});
