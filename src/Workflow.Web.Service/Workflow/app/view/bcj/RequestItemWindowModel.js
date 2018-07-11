/**
 *Author : Phanny 
 *
 */

Ext.define('Workflow.view.bcj.RequestItemWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.bcj-requestitemwindow',
    data: {
        item: null,
        unitPrice: 0,
        quantity: null
    },
    formulas:{
         readOnlyField : function(get){
             //To be implement
         }    
    }

});
