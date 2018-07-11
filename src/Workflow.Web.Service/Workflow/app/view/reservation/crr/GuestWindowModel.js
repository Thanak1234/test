/**
 *Author : cheaveasna 
 *
 */

Ext.define('Workflow.view.reservation.crr.GuestWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.crr-guestwindow',
    data: {
        name: null,
        title: null,
        companyName: null,
        requestHeaderId: 0,
        viewSetting: null
    },
    
    formulas:{
        readOnlyField: function (get) {
            console.log('guest windows model ', get('selectedItem'));
             if (get('selectedItem') && get('viewSetting') && get('viewSetting').guestBlock.addEdit) {
                 return false;
             } else {
                 return true;
             }
         }    
    }

});
