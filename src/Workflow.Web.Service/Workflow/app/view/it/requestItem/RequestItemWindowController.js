Ext.define('Workflow.view.it.requestItem.RequestItemWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.it-requestitem-requestitemwindow',
    
    onItemChanged: function (el) {
        
        var me = this,
            itemType = me.getReferences().it_item_type;
        
        var itemId = el.getSelection()?el.getSelection().get('id'):0 ;
        
        itemType.getStore().getProxy().extraParams = { itemId : itemId };
        itemType.getStore().load();
        
        this.itemChange();
    },
    
    onItemTypeChanged : function (){
       this.itemChange();
    },
    
    
   itemChange : function (){
       
       var me = this,
            references = me.getReferences(),
            item = references.it_item.getSelection(),
            itemId = item?item.get('id') : 0  ,
            itemTypeId = references.it_item_type.getSelection()?references.it_item_type.getSelection().get('id'): 0 ;
            
        references.it_item_role.clearValue();
        references.it_item_role.getStore().getProxy().extraParams = { itemId : itemId, itemTypeId : itemTypeId };
        references.it_item_role.getStore().load();
   },
   
   getFormItem : function(){
       var me       = this,
            model   = me.getView().getViewModel(),
            itemType= model.get('itemType'),
            item    = model.get('reqItem'),
            itemRole= model.get('itemRole'),
            qty     = model.get('qty'),
            comment = model.get('comment'),
            session = model.get('session');
       
       var requestItem = Ext.create('Workflow.model.itRequestForm.RequestItem', {
           itemId       : item.get('id'),
           itemName     : item.get('itemName'),   
           session      : session,
           itemTypeId   : itemType?itemType.get('id') : 0 ,
           itemTypeName : itemType? itemType.get('itemTypeName'): null,
           itemRoleId   : itemRole?itemRole.get('id') : 0,
           itemRoleName : itemRole?itemRole.get('roleName'): null ,
           qty          : qty ,
           comment      : comment
       });
       return requestItem;
   },

   moreValidation: function () {

       var me = this,
           valid = true,
           model         = me.getView().getViewModel(),
           itemType = model.get('itemType'),
           itemRole = model.get('itemRole'),
           itemTypeStore = model.getStore('itemTypeStore'),
           itemRoleStore = model.getStore('itemRoleStore');

       if (itemTypeStore.getCount() > 0 && !itemType) {
           valid = false;
           Ext.Msg.alert({
               title: 'Information',
               msg: 'Item type is required.',
               icon: Ext.MessageBox.ERROR
           });
       }

       if (itemRoleStore.getCount() > 0 && !itemRole) {
           valid = false;
           Ext.Msg.alert({
               title: 'Information',
               msg: 'Item role is required.',
               icon: Ext.MessageBox.ERROR
           });
       }

       return valid;
   }
    
});
