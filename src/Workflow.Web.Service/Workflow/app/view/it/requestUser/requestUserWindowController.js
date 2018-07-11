Ext.define('Workflow.view.it.requestUser.requestUserWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.it-requestuser-requestuserwindow',
    
    parseItem: function (){
        var me = this,
            record = me.getView().getViewModel().parseToUserRequestItem();
        return record;
          
    }
});
