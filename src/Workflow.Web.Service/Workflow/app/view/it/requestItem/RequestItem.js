
Ext.define("Workflow.view.it.requestItem.RequestItem",{
    extend: "Ext.grid.Panel",
    xtype: 'it-request-item',

    requires: [
        "Workflow.view.it.requestItem.RequestItemController",
        "Workflow.view.it.requestItem.RequestItemModel"
    ],

    controller: "it-requestitem-requestitem",
    viewModel: {
        type: "it-requestitem-requestitem"
    },
    listeners: {
            rowdblclick: 'viewItemAction'
    },
    iconCls: 'fa fa-list-alt',
    title: "IT Request List",
    stateful: true,
    collapsible: true,
    headerBorders: false, 
    bind: {
        selection: '{selectedItem}'//,
        //store: '{dataStore}'
    },
    viewConfig: {
        enableTextSelection: true
    },
    
    initComponent: function () {
        var me = this;
        
        me.tbar= [{
                xtype       : 'combo',
                queryMode   : 'local',
                displayField: 'sessionName',
                editable    : false,
                reference   : 'session',
                valueField  : 'id',
                fieldLabel  : 'IT Session',
                bind: {
                    selection: '{sessionSelection}',
                   // store    : '{sessionStore}',
                    disabled: '{!canAddRemove}'
                },
                listeners: {
                    beforeselect: 'onSessionBeforeChanged',
                    scope       : 'controller'
                }
            },{
                xtype: 'label',
                height:40,
                bind: {
                    html: '<font style="color:red; font-size:11px;">{sessionSelection.indicator}</font>'
                }
            },'->',{
                xtype   : 'button',
                text    : 'Add',
                iconCls : 'fa fa-plus-circle',
                handler : 'addItemAction',
                bind: {
                    disabled: '{!canAdd}'
                }
                
            },{
                xtype   : 'button',
                text    : 'Edit',
                bind: {
                    disabled: '{!editable}'
                },
                iconCls : 'fa fa-plus-circlefa fa-pencil-square-o',
                handler : 'editItemAction'
            },{
                xtype   : 'button',
                text    : 'View',
                bind: {
                    disabled: '{!canView}'
                },
                iconCls : 'fa fa-eye',
                handler : 'viewItemAction'
        }];
        
        me.columns = [
            {
                text        : 'SESSION',
                width       : 100,
                sortable    : true,
                dataIndex   : 'session'

            },{
                text        : 'ITEM NAME',
                width       : 200,
                sortable    : true,
                dataIndex   : 'itemName'
            },{
                text        : 'ITEM TYPE',
                width       : 150,
                sortable    : true,
                dataIndex   : 'itemTypeName'
            },{
                text        : 'ROLE',
                width       : 150,
                sortable    : true,
                dataIndex   : 'itemRoleName'
            },{
                text        : 'QTY',
                width       : 100,
                sortable    : true,
                dataIndex   : 'qty'
            },{
                text        : 'COMMENT',
                flex        : 1,
                sortable    : true,
                dataIndex   : 'comment'
            },{
                
                menuDisabled    : true,
                sortable        : false,
                width           : 50,
                xtype           : 'actioncolumn', 
                align           : 'center',
                bind                : {
                    hidden : '{!canAddRemove}'
                },
                items           : [{
                    iconCls: 'fa fa-trash-o',
                    tooltip: 'Remove',width: 150,
                    handler: 'removeRecord'
                }]
}
        ];
        
         me.callParent(arguments);
    }
});
