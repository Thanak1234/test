Ext.define('Workflow.view.it.requestItem.RequestItemModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.it-requestitem-requestitem',
    data: {
        selectedItem        : null,
        sessionSelection    : null,
        viewSetting: null
    },
    stores : {
        dataStore : {
             model   :   'Workflow.model.itRequestForm.RequestItem'
                 
        },
        sessionStore : { 
            model : 'Workflow.model.itRequestForm.Session',
            data  : [
                { "id": 3, "sessionName": "INFRASTRUCTURE", "indicator": "All request pertaining to request of new hardware/software/emails/<br/>email grouping/shared folder permission/internet bandwidth etc." },
                { "id": 1, "sessionName": "CASINO APPLICATION", "indicator": "CMP/TABLE VIEW/SDS/CAGE/CRM/BPS/ICS/K2" },
                { "id": 2, "sessionName": "HOTEL APPLICATION", "indicator": "New/delete/modify/password reset - (OPERA, SIMPHONY, MATERIAL CONTROL, VINGCARD, JDS and SUN)" }
            ]
        }  
    },
    formulas:{
        
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
            if (get('sessionSelection') && get('viewSetting') && get('viewSetting').requestItemBlock.addEdit) {
                return true;
            }else{
                return false;
            }
        },
        
        sessionCanChanged : function(get){
            return get('dataStore').getCount()==0;
        }
    }

});
