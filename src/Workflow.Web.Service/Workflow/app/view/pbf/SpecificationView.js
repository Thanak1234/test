/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.pbf.Specification", {
    extend: "Ext.grid.Panel",
    xtype: "pbf-specification-view",
    
    requires: [
        "Workflow.view.pbf.SpecificationController",
        "Workflow.view.pbf.SpecificationModel"
    ],
    iconCls: 'fa fa-gears',
    title: 'Item Request',
    controller: "pbf-specification",
    viewModel: {
        type: "pbf-specification"
    },
    listeners: {
        rowdblclick: 'viewItemAction'
    },
    bind: {
        selection   : '{selectedItem}'
        //store       : '{dataStore}'
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
                    disabled: '{!canAdd}'
                }
                
            },{
                xtype   : 'button',
                text    : 'Edit',
                bind    : {
                    disabled: '{!addEdit}'
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
                text: 'Item Name',
                width: 250,
                sortable: true,
                dataIndex: 'name'
            }, {
                text: 'Specificity (size, format, etc.)',
                flex: 1,
                sortable: true,
                dataIndex: 'description'
            }, {
                text: 'QTY',
                sortable: true,
                width: 200,
                dataIndex: 'quantity'
            }, {
                //xtype: 'numbercolumn',
                text: 'Item Nr.',
                width: 250,
                sortable: true,
                dataIndex: 'itemRef',
                bind: {
                    hidden: '{!visibleNrItem}'
                }
            }, {
                
                menuDisabled: true,
                sortable: false,
                width       : 50,
                xtype: 'actioncolumn', 
                align : 'center',
                bind    : {
                    hidden: '{!addEdit}'
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
