/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.bcj.AnalysisItemView", {
    extend: "Ext.grid.Panel",
    xtype: "bcj-analysis-item-view",
    
    requires: [
        "Workflow.view.bcj.AnalysisItemViewController",
        "Workflow.view.bcj.AnalysisItemViewModel"
    ],
    title: 'Financial Analysis',
    controller: "bcj-analysisitemview",
    viewModel: {
        type: "bcj-analysisitemview"
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
                    disabled : '{!canAddRemove}'
                }
                
            },{
                xtype   : 'button',
                text    : 'Edit',
                bind    : {
                    disabled: '{disabled}'
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

         me.dockedItems = [{
             xtype: 'toolbar',
             dock: 'bottom',
             items: [
                 '->', {
                     xtype: 'label',
                     margin: '0 60 0 0',
                     bind: {
                         text: '{totalAmount}'
                     }
                 }
             ]
         }];
        me.columns = [
            {
                text: 'DESCRIPTION',
                width: 200,
                sortable: true,
                dataIndex: 'description'
            }, {
                xtype: 'numbercolumn',
                text: 'REVENUE',
                width: 250,
                sortable: true,
                dataIndex: 'revenue',
                format: '$0,000.00',
                align: 'right'

            },{
                text: 'QUANTITY',
                sortable: true,
                flex: 1,
                dataIndex: 'quantity'
            }, {
                xtype: 'numbercolumn',
                text: 'TOTAL',
                sortable: true,
                flex: 1,
                dataIndex: 'total',
                format: '$0,000.00',
                align: 'right'
            }, {
                
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
