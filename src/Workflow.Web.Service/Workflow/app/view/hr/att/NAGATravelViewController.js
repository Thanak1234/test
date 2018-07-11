Ext.define('Workflow.view.hr.att.NAGATravelViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.att-nagatravelview',
    
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
        if (data.flightDetail) {
            model.set('flightDetail', data.flightDetail);            
        }
        model.set('viewSetting', data.viewSetting);
    },
    clearData : function(){
        this.getView().reset();
    }
});
