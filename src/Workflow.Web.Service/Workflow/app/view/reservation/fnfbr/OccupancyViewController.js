/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.fnfbr.OccupancyViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.fnf-occupancyview',
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    
    loadData: function(data){
        var me  = this,
            grid = me.getView();

        var dataStore = this.getView().getViewModel().getStore('occupancy');
        if (data.data && data.data.length > 0) {
            dataStore.setData(data.data);
        }
        
        me.getView().setStore(dataStore);
        me.getView().getViewModel().set('viewSetting', data.viewSetting);
    },
    clearData: function () {
       this.getView().getStore().removeAll();
    }
});
