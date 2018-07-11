/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.av.RequestItemViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.av-requestitemview',
    data: {
        selectedItem    : null,
        viewSetting     : null
    },
    stores : {
        dataStore : {
            model : 'Workflow.model.avForm.RequestItem'
        }
    },
    formulas : {
         canAddRemove : function(get){
            
            if(get('viewSetting') &&  get('viewSetting').requestItemBlock.addEdit){
                return true
            }else{
                return false;
            }  
        },
        
        editable : function(get) {
            if(get('selectedItem') && get('viewSetting')  &&  get('viewSetting').requestItemBlock.addEdit ){
                return true;
            }else{
                return false;
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
