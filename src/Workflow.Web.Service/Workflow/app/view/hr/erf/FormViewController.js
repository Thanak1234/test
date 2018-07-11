/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.hr.erf.FormViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.erf-form',
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    loadData: function (data) {
        var me = this,
            view = me.getView(),
            model = me.getView().getViewModel();

        me.view.getForm().reset(); // clean form before bind model.
        if (data.requisition) {
            model.set('requisition', data.requisition);
        }
        model.set('viewSetting', data.viewSetting);
    },
    clearData : function(){
        this.getView().reset();
    }
});
