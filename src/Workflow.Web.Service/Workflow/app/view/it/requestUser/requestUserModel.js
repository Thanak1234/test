Ext.define('Workflow.view.it.requestUser.requestUserModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.it-requestuser-requestuser',
    data: {
        selectedItem: null,
        viewSetting: null
    },
    stores : {
        userStore : {
             model: 'Workflow.model.itRequestForm.RequestUser'
        }
    },
    formulas:{
        
        canAddRemove : function(get){
            
          if(get('viewSetting') &&  get('viewSetting').requestUserBlock.addEdit){
              return true
          }else{
              return false;
          }  
        },
        
        editable : function(get) {
            if( get('selectedItem') && get('viewSetting')  &&  get('viewSetting').requestUserBlock.addEdit ){
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
            
        }
    }

});
