Ext.define("Workflow.view.hr.att.FlightDetailView", {
    extend: "Ext.grid.Panel",
    xtype: "att-flightdetailview",
    
    requires: [
        "Workflow.view.hr.att.FlightDetailViewController",
        "Workflow.view.hr.att.FlightDetailViewModel"
    ],
    title: 'Flight Details',
    iconCls: 'fa fa-plane',
    controller: "att-flightdetailview",
    viewModel: {
        type: "att-flightdetailview"
    },
    listeners: {
        rowdblclick: 'viewFlightDetailAction'
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
            handler: 'addFlightDetailAction',
            bind: {
                disabled: '{!canAddRemoveFlightDetail}'
            }

        }, {
            xtype: 'button',
            text: 'Edit',
            bind: {
                disabled: '{!editableFlightDetail}'
            },
            iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
            handler: 'editFlightDetailAction'
        }, {
            xtype: 'button',
            text: 'View',
            bind: {
                disabled: '{!canViewFlightDetail}'
            },
            iconCls: 'fa fa-eye',
            handler: 'viewFlightDetailAction'
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
                    hidden: '{!canAddRemoveFlightDetail}'
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
