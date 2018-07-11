/* global Ext */
Ext.define('Workflow.view.it.requestItem.RequestItemController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.it-requestitem-requestitem',
    
     config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    
    loadData: function(data){
        var me  = this;
        var dataStore = this.getView().getViewModel().getStore('dataStore');
        var sessionStore = this.getView().getViewModel().getStore('sessionStore');
         
        if(data.data &&  data.data.length>0){
            var item = data.data[0];
            var session = Ext.create('Workflow.model.itRequestForm.Session', { id : item.sessionId , sessionName : item.session } );
            me.getView().getViewModel().set('sessionSelection',session );
            dataStore.setData(data.data);
        }
        
        me.getView().getReferences().session.setStore(sessionStore);
        me.getView().setStore(dataStore);
        me.getView().getViewModel().set('viewSetting', data.viewSetting );    
        
    },
    
    clearData : function(){
        this.getView().getStore().removeAll();
    },
    
    
    onSessionBeforeChanged : function(el, newValue, oldValue, eOpt){
        
        if(!el.getSelection()) {
            return ;
        }
        
        var me =this, dataStore = me.getView().getStore();
      
        if(dataStore.getCount()>0){
            Ext.MessageBox.show({
                title   : 'Alert',
                msg     : 'Please clear all request items before changing the session. It request form does not support multi sessions in single request.',
                icon    : Ext.MessageBox.INFO,
                scope   : this
            });
            return false;
        }   
    },
    
    
    addItemAction : function(el, e, eOpts){
        var me = this,
            session = me.getReferences().session,
            sessionId = session.getSelection()?session.getSelection().get('id'): 0,
            sessionName=  session.getSelection()?session.getSelection().get('sessionName'): null;
            
        me.showWindowDialog(el, { action: 'ADD',item:  null, sessionId:sessionId, session : sessionName  }, 'Add Request Item', function(record){
        
           if(me.checkExisting(record)){
               Ext.Msg.alert({title: 'Information', msg: 'The item is already added.'});
                return false;
            } 
            me.getView().getStore().add(record);
            return true;
        });
    },
    
    editItemAction : function(el, e, eOpts){
        var me = this,
            session         = me.getReferences().session,
            sessionId       = session.getSelection()?session.getSelection().get('id'): 0,
            sessionName     = session.getSelection()?session.getSelection().get('sessionName'): null,
            selectedRecord  = me.getParseSelectedItem(),
            id              = selectedRecord.id;
        
        me.showWindowDialog(el, { 
                action      : 'Edit',
                reqItem     : selectedRecord.item,
                itemType    : selectedRecord.itemType,
                itemRole    : selectedRecord.itemRole,
                qty         : selectedRecord.qty,
                comment     : selectedRecord.comment,
                sessionId   : sessionId, 
                session     : sessionName
                
            }, 'Edit Request Item', function(record){
        
            var currentRecord =  me.getView().getViewModel().get('selectedItem');

            if( (currentRecord.get('itemId')!=record.get('itemId')
                ||  currentRecord.get('itemTypeId')!=record.get('itemTypeId')
                || currentRecord.get('itemRoleId')!=record.get('itemRoleId') )
                && me.checkExisting(record)  ){   
                     
               Ext.Msg.alert({title: 'Information', msg: 'The item already exists.'});
               return false;
            } 
            currentRecord.set(record.getData());
            currentRecord.set('id',id);
            return true;
        });
    },
    
    checkExisting: function (record) {
        
        var itemStore = this.getView().getStore(); 
        
        return itemStore.query( 'itemId', record.get('itemId')).items.length>0
                && itemStore.query( 'itemTypeId', record.get('itemTypeId')).items.length>0
                && itemStore.query( 'itemRoleId', record.get('itemRoleId')).items.length>0;
        
    },
    viewItemAction : function (el, e, eOpts) {
        var me              = this,
            session         = me.getReferences().session,
            sessionId       = session.getSelection()?session.getSelection().get('id'): 0;
        var selectedRecord = me.getParseSelectedItem();

        var viewmodel = me.getView().getViewModel(),
            editable = viewmodel.get('editable');

        if (editable) {
            me.editItemAction(el);
        } else {
            me.showWindowDialog(el, {
                action: 'VIEW',
                sessionId: sessionId,
                reqItem: selectedRecord.item,
                itemType: selectedRecord.itemType,
                itemRole: selectedRecord.itemRole,
                qty: selectedRecord.qty,
                comment: selectedRecord.comment

            }, 'View Request Item', function (record) {

                return true;
            });
        }
    },
    getParseSelectedItem: function(){
         var me = this,
          selectedRecord  = me.getView().getViewModel().get('selectedItem')  ;
            
        var item = Ext.create('Workflow.model.itRequestForm.Item', {
                id          : selectedRecord.get('itemId'),
                itemName    : selectedRecord.get('itemName')
             });
             
             
             
        var itemType = Ext.create('Workflow.model.itRequestForm.ItemType',{
            id              : selectedRecord.get('itemTypeId'),
            itemTypeName    : selectedRecord.get('itemTypeName')
        });     
        var itemRole = Ext.create('Workflow.model.itRequestForm.ItemRole',{
            id      : selectedRecord.get('itemRoleId'),
            roleName: selectedRecord.get('itemRoleName')
        }); 
        
        return {
                id          : selectedRecord.get('id'),
                item        : item,
                itemType    : itemType, 
                itemRole    :itemRole, 
                qty         : selectedRecord.get('qty'), 
                comment     : selectedRecord.get('comment')  
            };
        
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
    
    showWindowDialog: function(lauchFromel, model, title, cb ){
        
        var me = this;
        var window = Ext.create('Workflow.view.it.requestItem.RequestItemWindow', 
         {
            title       : title, 
            mainView    : me,
            viewModel   : {
                data    : model
            }, 
            lauchFrom   : lauchFromel,
            cbFn        : cb
        });
   
        window.show(lauchFromel);
    }
    
});
