/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.pbf.SpecificationModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.pbf-specification',
    data: {
        viewSetting: null,
        selectedItem: null
    },
    stores : {
        dataStore : {
            model: 'Workflow.model.pbfForm.Specification'
        }
    },
    formulas : {
         canAdd : function(get){
             if (get('viewSetting') && get('viewSetting').specificationBlock.addOnly) {
                 return true;
            }else{
                return false;
            }  
         },
         addEdit: function (get) {
            if (get('selectedItem') && get('viewSetting') && get('viewSetting').specificationBlock.addEdit) {
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
        visibleNrItem: function (get) {
            console.log('specificationBlock.visibleNr', get('viewSetting') && get('viewSetting').specificationBlock.visibleNr);
            if (get('viewSetting') && get('viewSetting').specificationBlock.visibleNr) {
                return true;
            } else {
                return false;
            }
        }
    }

});
