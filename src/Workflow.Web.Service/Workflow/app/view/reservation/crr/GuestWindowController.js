/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.reservation.crr.GuestWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.crr-guestwindow',
    
    getFormItem : function (){
        var me      = this,
            model   =  me.getView().getViewModel();
            
        var gridGuest = Ext.create('Workflow.model.crrForm.Guest', {
            //id: model.get('id') ? model.get('id') : 0,
            name: model.get('name'),
            title: model.get('title'),
            companyName: model.get('companyName'),
            requestHeaderId: model.get('requestHeaderId') ? model.get('requestHeaderId'):0
            
        });
        console.log('tes ', gridGuest)
        return gridGuest;
    }
    
});
