Ext.define("Workflow.view.hr.att.DestinationView", {
    extend: "Ext.grid.Panel",
    xtype: "att-destinationview",
    reference: 'destinationView',
    requires: [
        "Workflow.view.hr.att.DestinationViewController",
        "Workflow.view.hr.att.DestinationViewModel"
    ],
    title: 'Flight Details',
    iconCls: 'fa fa-plane',
    controller: "att-destinationview",
    viewModel: {
        type: "att-destinationview"
    },
    listeners: {
        rowdblclick: 'viewDestinationAction'
    },
    bind: {
        selection: '{selectedItem}',
        store: '{dataStore}'
    },
    viewConfig: {
        enableTextSelection: true
    },
    stateful: true,
    collapsible: true,
    headerBorders: false,

    initComponent: function () {
        var me = this;
        me.tbar = ['->', {
            xtype: 'button',
            text: 'Add',
            iconCls: 'fa fa-plus-circle',
            handler: 'addDestinationAction',
            bind: {
                disabled: '{!canAddRemoveDestination}'
            }

        }, {
            xtype: 'button',
            text: 'Edit',
            bind: {
                disabled: '{!editableDestination}'
            },
            iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
            handler: 'editDestinationAction'
        }, {
            xtype: 'button',
            text: 'View',
            bind: {
                disabled: '{!canViewDestination}'
            },
            iconCls: 'fa fa-eye',
            handler: 'viewDestinationAction'
        }];
        me.columns = [
            {
                text: 'From (Airpot)',
                width: 200,
                sortable: true,
                dataIndex: 'fromDestination'
            }, {
                text: 'To (Airpot)',
                width: 200,
                sortable: true,
                dataIndex: 'toDestination'
            }, {
                text: 'Date',
                
                width: 250,
                sortable: true,
                dataIndex: 'date',
                renderer: function (v) {
                    return Ext.util.Format.date(v, 'Y-m-d');
                }

            }, {
                text: 'Time',                
                sortable: true,
                flex: 1,
                dataIndex: 'time',
                renderer: function (v) {
                    return Ext.util.Format.date(v, 'H:i A');
                }
            }, {

                menuDisabled: true,
                sortable: false,
                width: 50,
                xtype: 'actioncolumn',
                align: 'center',
                bind: {
                    hidden: '{!canAddRemoveDestination}'
                },
                items: [{
                    iconCls: 'fa fa-trash-o',
                    tooltip: 'Remove',
                    width: 150,
                    handler: 'removeRecord'
                }]
            }
        ];

        me.callParent(arguments);
    }

});
