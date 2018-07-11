/**
 *Author : Phanny 
 *
 */

Ext.define('Workflow.view.bcj.AnalysisItemWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.bcj-analysisItemwindow',
    data: {
        description: null,
        revenue: 0,
        quantity: null
    },
    formulas:{
         readOnlyField : function(get){
             //To be implement
         }
    }

});
