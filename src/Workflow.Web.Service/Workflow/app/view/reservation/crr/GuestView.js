/**
 *Author : veasna 
 *
 */
Ext.define("Workflow.view.reservation.crr.GuestView", {
    extend: "Ext.grid.Panel",
    xtype: "crr-guestview",

    requires: [
        "Workflow.view.reservation.crr.GuestViewController",
        "Workflow.view.reservation.crr.GuestViewModel"
    ],
    title: 'Guest',
    iconCls: 'x-fa fa-users',
    controller: "crr-guestview",
    viewModel: {
        type: "crr-guestview"
    },
    listeners: {
        rowdblclick: 'viewItemAction'
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
            iconCls: 'x-fa fa-plus-circle',
            handler: 'addGuestAction',
            bind: {
                disabled: '{!canAddRemove}'
            }

        }, {
            xtype: 'button',
            text: 'Edit',
            bind: {
                disabled: '{!editable}'
            },
            iconCls: 'x-fa fa-plus-circlefa fa-pencil-square-o',
            handler: 'editItemAction'
        }, {
            xtype: 'button',
            text: 'View',
            bind: {
                disabled: '{!canView}'
            },
            iconCls: 'x-fa fa-eye',
            handler: 'viewItemAction'
        }];
        me.columns = [
            {
                text: 'Guest Name',
                width: 200,
                sortable: true,
                dataIndex: 'name'
            }, {
                text: 'Position',
                width: 250,
                sortable: true,
                dataIndex: 'title'

            }, {
                text: 'Company Name',
                sortable: true,
                flex: 1,
                dataIndex: 'companyName'
            }, {

                menuDisabled: true,
                sortable: false,
                width: 50,
                xtype: 'actioncolumn',
                align: 'center',
                bind: {
                    hidden: '{!canAddRemove}'
                },
                items: [{
                    iconCls: 'x-fa fa-trash-o',
                    tooltip: 'Remove',
                    width: 150,
                    handler: 'removeRecord'
                }]
            }
        ];

        me.callParent(arguments);
    }

});
