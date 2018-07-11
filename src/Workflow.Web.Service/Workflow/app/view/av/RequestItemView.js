/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.av.RequestItemView",{
    extend: "Ext.grid.Panel",
    xtype : "av-request-item-view",
    
    requires: [
        "Workflow.view.av.RequestItemViewController",
        "Workflow.view.av.RequestItemViewModel"
    ],
    title: 'Scope Needed',
    iconCls: 'fa fa-list-alt',
    controller: "av-requestitemview",
    viewModel: {
        type: "av-requestitemview"
    },
    listeners: {
        rowdblclick: 'viewItemAction'
    },
    bind: {
        selection   : '{selectedItem}'
    },
    viewConfig: {
        enableTextSelection: true
    },
    stateful: true,
    collapsible: true,
    headerBorders: false, 
    
    initComponent: function () {
        var me = this;
         me.tbar= ['->',{
                xtype   : 'button',
                text    : 'Add',
                iconCls : 'fa fa-plus-circle',
                handler : 'addUserAction',
                bind    : {
                    disabled : '{!canAddRemove}'
                }
                
            },{
                xtype   : 'button',
                text    : 'Edit',
                bind    : {
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
                text        : 'ITEM TYPE',
                width       : 200,
                sortable    : true,
                dataIndex   : 'itemTypeName'
            },{
                text        : 'ITEM NAME',
                width       : 250,
                sortable    : true,
                dataIndex   : 'itemName'

            }, {
                text: 'QUANTITY',
                width: 100,
                dataIndex: 'quantity'
            }, {
                text        : 'COMMENT',
                sortable    : true,
                flex        : 1 ,
                dataIndex   : 'comment'
            },{
                
                menuDisabled: true,
                sortable: false,
                width       : 50,
                xtype: 'actioncolumn', 
                align : 'center',
                bind    : {
                            hidden : '{!canAddRemove}'
                        },
                items: [{
                        iconCls: 'fa fa-trash-o',
                        tooltip: 'Remove',width: 150,
                        handler: 'removeRecord'
                        }]
            }
        ];
        
         me.callParent(arguments);
    }
    
});
