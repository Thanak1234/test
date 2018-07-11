/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.common.activity.ActivityHistoryController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-activity-activityhistory',
    
    control: {
        '*': {
            loadData: 'loadData',
            onDataClear : 'clearData'
        }      
    },
    loadData :  function(data){
        
        var dataStore = this.getView().getViewModel().getStore('dataStore');
        
        if(data){
            dataStore.setData(data);
            dataStore.sort('actionDate', 'ASC');
        }
        
        this.getView().setStore(dataStore);
    },
    clearData : function (){
       //No data to be cleared
    },
    
    viewItemAction: function (el) {
        var selectedItem = this.getView().getViewModel().get('selectedItem');
        this.showWindowDialog(el, { action: 'VIEW', item: selectedItem }, 'View Activity History', function () {

        });
    },


    //TODO: consider abstract method or mixin
    showWindowDialog: function(lauchFromel, model, title, cb ){
        
        var me = this;
        var window = Ext.create('Workflow.view.common.activity.activityHistoryWindow.ActivityHistoryWindow',
         {
             title : title, 
             mainView: me,
             viewModel: {
                 data: model
             }, 
             lauchFrom: lauchFromel,
             cbFn: cb
         });
   
        window.show(lauchFromel);
    }
    
});
