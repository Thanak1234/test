
Ext.define("Workflow.view.admin.FingerPrintMonitor",{
    extend: "Ext.panel.Panel",
    controller: "admin-fingerprintmonitor",
    xtype: 'admin-fingerprintmonitor',
    viewModel: {
        type: "admin-fingerprintmonitor"
    },
    header: {
        hidden: true
    },
    ngconfig: {
        layout: 'fullScreen'
    },
    layout: 'fit',
    bodyBorder: false,
    initComponent: function() {
        var me = this;
        me.buildItems();
        me.callParent(arguments);        
    },
    buildItems: function() {
        var me = this;
        me.items = [
        {
            xtype: 'panel',
            autoScroll: true,
            defaults: {
                border: true
            },
            layout: 'fit',
            items: [
                {
                    xtype: 'grid',
                    reference: 'defaultGroup',
                    flex: 1,
                    bind: {
                        selection: '{selectedRow}',
                        store: '{fingerprintStore}'
                    },
                    tbar: [
                        {
                            xtype: 'button',
                            text: 'Refresh',
                            iconCls: 'fa fa-refresh',
                            handler: 'onRefreshClick'
                        },
                        '->',
                        {
                            xtype: 'button',
                            text: 'PENDING...',
                            iconCls: 'fa fa-cogs',
                            handler: 'onRestartClick',
                            disabled: true,
                            bind: {
                                disabled: '{disabled}',
                                text: '{buttonText}'
                            }
                        }                        
                    ],
                    columns: [
                        {
                            xtype: 'rownumberer'
                        },
                        {
                            xtype: 'actioncolumn',
                            width: 50,
                            sortable: false,
                            menuDisabled: true,
                            align: 'center',
                            dataIndex: 'Status',
                            items: [{
                                getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                                    return v == 'CONNECTED' ? 'fa fa-circle action-column-blue-color' : 'fa fa-circle action-column-red-color';
                                }
                            }]
                        },
                        {
                            text: "IP",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'IP'
                        },
                        {
                            text: "PORT",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'Port'
                        },
                        {
                            text: "MACHINE NUMBER",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'MachineNo'
                        },
                        {
                            text: "CONNECTED DATE",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'ConnectedDate',
                            renderer: function (value) {
                                return Ext.util.Format.date(value, 'Y-m-d H:i:s');
                            }
                        },
                        {
                            text: "LAST CONNECTED DATE",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'LastConnectedDate',
                            renderer: function (value) {
                                return Ext.util.Format.date(value, 'Y-m-d H:i:s');
                            }
                        },
                        {
                            text: "REMARK",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'Remark'
                        },
                        {
                            text: "STATUS",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'Status'
                        },
                        {
                            text: "ACTIVE",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'Active',
                            hidden: true
                        },
                        {
                            width: 50,
                            xtype: 'widgetcolumn',
                            menuDisabled: true,
                            widget: {
                                xtype: 'button',
                                textAlign: 'left',
                                width: 28,
                                height: 29,
                                arrowCls: '',
                                iconCls: 'fa fa-chevron-right'
                            },
                            onWidgetAttach: function (column, widget, record) {
                                var data = record.getData();

                                var status = data.Status;
                                var obj = { Data: data.IP };

                                var menus = [];

                                if (status == 'CONNECTED') {
                                    obj.Command = 1;
                                    menus.push(
                                         {
                                             text: 'DISCONNECT',
                                             iconCls: 'fa fa-stop',
                                             jsonData: obj,
                                             handler: 'onActionClick'
                                         }
                                    );
                                } else {
                                    obj.Command = 0;
                                    menus.push(
                                         {
                                             text: 'RECONNECT',
                                             iconCls: 'fa fa-play',
                                             jsonData: obj,
                                             handler: 'onActionClick'
                                         }
                                    );
                                }
                                widget.setMenu(menus);
                            }
                        }
                    ]
                }
            ]
        }];
    }
});
