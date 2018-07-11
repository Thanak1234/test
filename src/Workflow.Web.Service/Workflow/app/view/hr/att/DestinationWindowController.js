Ext.define('Workflow.view.hr.att.DestinationWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.att-destinationwindow',
    
    getFormItem : function (){
        var me      = this,
            model   =  me.getView().getViewModel();
            
        var destination = Ext.create('Workflow.model.attForm.Destination', {
            //id: model.get('id') ? model.get('id') : 0,
            fromDestination: model.get('fromDestination'),
            toDestination: model.get('toDestination'),
            date: model.get('date'),
            time: model.get('time'),
            requestHeaderId: model.get('requestHeaderId') ? model.get('requestHeaderId'):0
            
        });        
        return destination;
    }
    
});
