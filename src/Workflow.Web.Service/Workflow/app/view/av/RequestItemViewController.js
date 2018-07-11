/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.av.RequestItemViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.av-requestitemview',
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    
    loadData: function(data){
        var me = this,
            view = this.getView(),
            viewmodel = this.getView().getViewModel();
        var dataStore = this.getView().getViewModel().getStore('dataStore');

        if(data.data &&  data.data.length>0){
            //Load data to from
            dataStore.setData(data.data);
            dataStore.filter([{ property: 'itemTypeName', value: view.scopeType }]);
        }
        
        me.getView().setStore(dataStore);
        me.getView().getViewModel().set('viewSetting', data.viewSetting );    
        
    },
    
     clearData : function(){
       this.getView().getStore().removeAll();
    },
    addUserAction : function (el, e, eOpts) {
        var me = this;
        var view = me.getView();
        me.showWindowDialog(el, 'Workflow.view.av.RequestItemWindow', {
            action: 'ADD',
            scopeType: view.scopeType
        }, ('Add Scope Needed - ' + view.scopeType), function (record) {
           if(me.checkExisting(record)){
               Ext.Msg.alert({
                   title: 'Information', 
                   msg: 'The item is already added.',
                   icon    : Ext.MessageBox.ERROR
                });
                return false;
            } 
            me.getView().getStore().add(record);
            return true;
        });
    },
    
    editItemAction : function(el, e, eOpts){
        var me = this,
            view = me.getView(),
            selectedModel = me.getView().getViewModel().get('selectedItem');
            me.showWindowDialog(el,'Workflow.view.av.RequestItemWindow',  { 
                        action      : 'EDIT',
                        itemId: selectedModel.get('itemId'),
                        //itemName: selectedModel.get('itemName'),
                        quantity: selectedModel.get('quantity'),
                        comment: selectedModel.get('comment'),
                        scopeType : view.scopeType
            }, ('Edit Scope Needed - ' + view.scopeType), function (record) {
            if( selectedModel.get('itemId') !==record.get('itemId')  && me.checkExisting(record)){
               Ext.Msg.alert({
                   title: 'Information', 
                   msg: 'The item is already added.',
                   icon    : Ext.MessageBox.ERROR
                });
                return false;
            } 
            
            selectedModel.set(record.getData());
            selectedModel.set('id', selectedModel.get('id'));
            return true;
            });
    },
    
    viewItemAction : function (el) {
        var me = this,
            selectedModel = me.getView().getViewModel().get('selectedItem'),
            view = me.getView();

        me.showWindowDialog(el,'Workflow.view.av.RequestItemWindow',  { 
                action: 'VIEW',
                itemId: selectedModel.get('itemId'),
                //itemName: selectedModel.get('itemName'),
                quantity: selectedModel.get('quantity'),
                comment: selectedModel.get('comment'),
                scopeType: view.scopeType
        }, ('View Scope Needed - ' + view.scopeType), function (record) {
            return true;
        });
    },
    removeRecord: function (grid, rowIndex, colIndex){
        var me      = this,
            store   = grid.getStore(), 
            rec     = store.getAt(rowIndex);
            
        Ext.MessageBox.show({
            title       : 'Alert',
            msg         : 'Are you sure to delete this record?',
            buttons     : Ext.MessageBox.YESNO,
            icon        : Ext.MessageBox.QUESTION,
            scope       : this,
            fn          : function(bt){
                            if(bt==='yes') {
                            store.remove(rec);
                            me.showToast(Ext.String.format('Request item {0} has been removed', rec.get('itemName')));
                         }
            } 
        });
    },
    
    checkExisting : function record(record){
        var itemStore = this.getView().getStore(); 
        return itemStore.query( 'itemId', record.get('itemId')).items.length>0
    }
    
    
    
});
