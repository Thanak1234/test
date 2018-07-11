
Ext.define("Workflow.view.it.requestItem.RequestItemWindow",{
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.it.requestItem.RequestItemWindowController",
        "Workflow.view.it.requestItem.RequestItemWindowModel"
    ],

    controller: "it-requestitem-requestitemwindow",
    viewModel: {
        type: "it-requestitem-requestitemwindow"
    },
    layout: 'fit',
	maximizable: true,
	//maximized: true,
    initComponent: function () {
        var me = this;
         me.items=[{
            xtype: 'form',
            frame: false,
            bodyPadding: '10 10 0',
            reference: 'form', 
            method: 'POST',
            defaultListenerScope: true,
            defaults: {
                flex        : 1,
                anchor      : '100%', 
                msgTarget   : 'side',
                labelWidth  : 150, 
                layout      : 'form', 
                xtype       : 'container'  
            },  
            items: [
                        {
                            fieldLabel  : 'Item',
                            xtype       : 'combo',
                            displayField: 'itemName',
                            valueField  : 'id',
                            reference   : 'it_item',
                            //editable    : false,
                            queryMode   : 'local',
                            typeAhead   : true,
                            allowBlank  : false,
                            forceSelection : true,
                            listeners: {
                                change  : 'onItemChanged',
                                scope   : 'controller'
                            },
                            bind        : {
                                selection   : '{reqItem}',
                                store       : '{itemStore}',
                                readOnly    : '{readOnlyField}'
                            } 
                            
                        },{
                            fieldLabel  : 'Item Type',
                            displayField: 'itemTypeName',
                            valueField  : 'id',
                            queryMode   : 'local',
                            typeAhead   : true,
                            reference   : 'it_item_type',
                            xtype       : 'combo',
                            forceSelection : true,
                            
                            listeners: {
                                change: 'onItemTypeChanged',
                                scope: 'controller'
                            },
                            bind        : {
                                selection : '{itemType}',
                                store     : '{itemTypeStore}',
                                readOnly: '{readOnlyField}'
                            } 
                        },{
                            fieldLabel  : 'Role',
                            displayField: 'roleName',
                            valueField  : 'id',
                            reference   : 'it_item_role',
                            xtype       : 'combo',
                            queryMode   : 'local',
                            typeAhead   : true,
                            //store       : itemRoleStore,
                            forceSelection : true,
                            bind        : {
                                selection   : '{itemRole}',
                                store       : '{itemRoleStore}',
                                readOnly    : '{readOnlyField}'
                            } 
                            
                        },{
                            fieldLabel  : 'Qty',
                            xtype       : 'numberfield',
                            value       : 1,
                            minValue    : 1,
                            maxValue    : 1000,
                            allowBlank  : false,
                            bind        : { value: '{qty}', readOnly: '{readOnlyField}' }
                        },{
                            fieldLabel  : 'Comment',
                            xtype       : 'textarea',
							anchor: '100% -220', 
							margin:'20 0 10', 
                            bind        : { value: '{comment}', readOnly: '{readOnlyField}'}
                        }, {
                            xtype: 'label',
                            text: '* If characters more than 2000, please attach your file for detail',
                            margin : '0 150'
                        }
                ] 
        }];
        me.callParent(arguments);
        //Load data to all combo box
        var itemStore = me.getViewModel().getStore('itemStore');
        itemStore.getProxy().extraParams = { sessionId : me.getViewModel().get('sessionId') };
        itemStore.load();
        
        
        var itemTypeStore = me.getViewModel().getStore('itemTypeStore');
        var item = me.getViewModel().get('reqItem');
        var itemId = item?item.get('id'): 0;
        itemTypeStore.getProxy().extraParams = { itemId : itemId };
        itemTypeStore.load();
        
        var itemRoleStore = me.getViewModel().getStore('itemRoleStore');
        var itemType= me.getViewModel().get('itemType');
        var itemTypeId = itemType? itemType.get('id'): 0;
        itemRoleStore.getProxy().extraParams = { itemId : itemId,  itemTypeId : itemTypeId };
        itemRoleStore.load();
        
    }
});
