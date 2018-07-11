/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.pbf.FormViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.pbf-form',
    
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
        if (data.projectBrief) {
            model.set('projectBrief', data.projectBrief);
        }
        model.set('viewSetting', data.viewSetting);
    },
    clearData : function(){
        this.getView().reset();
    }
});
