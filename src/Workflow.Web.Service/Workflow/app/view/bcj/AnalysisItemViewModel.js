/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.bcj.AnalysisItemViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.bcj-analysisitemview',
    data: {
        selectedItem    : null,
        viewSetting: null,
        totalAmount: 0
    },
    stores : {
        dataStore : {
            model: 'Workflow.model.bcjForm.AnalysisItem'
        }
    },
    formulas : {
         canAddRemove : function(get){
            
             if (get('viewSetting') && get('viewSetting').analysisItemBlock.addEdit) {
                return true
            }else{
                return false;
            }  
        },
        
        editable : function(get) {
            if (get('selectedItem') && get('viewSetting') && get('viewSetting').analysisItemBlock.addEdit) {
                return true;
            }else{
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
