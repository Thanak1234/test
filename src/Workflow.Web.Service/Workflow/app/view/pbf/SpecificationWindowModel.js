/**
 *Author : Phanny 
 *
 */

Ext.define('Workflow.view.pbf.SpecificationWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.pbf-specificationwindow',
    data: {
        description: null,
        name: null,
        quantity: null,
        itemId: 0,
        itemRef: null,
        viewSetting: null
    },
    formulas:{
         readOnlyField : function(get){
             //To be implement
         },
         visibleNr: function (get) {             
             if (get('viewSetting') && get('viewSetting').specificationBlock.visibleNr) {
                 return true;
             } else {
                 return false;
             }
         }
    }

});
