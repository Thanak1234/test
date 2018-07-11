Ext.define('Workflow.view.common.worklist.Draft', {
    extend: 'Ext.grid.Panel',
    xtype: 'draftlist',
    requires: [
        'Ext.grid.feature.Feature'
    ],
    store: Ext.create('Ext.data.Store', {
        autoLoad: true,
        proxy: {
            type: 'rest',
            url: '/api/dashboard/tasks/drafted',

            reader: {
                type: 'json'
            }
        }
    }),
    bind: {
        selection: '{selectedRecord}'
    },
    //listeners: {
    //    rowdblclick: 'viewItemAction',
    //    itemclick: 'onItemclick',
    //    sortchange: 'onSortChange',
    //    headerclick: 'onHeaderClick'
    //},
    columnLines: true,
    initComponent: function () {
        var me = this;
        me.buildGridColumns();

        me.callParent(arguments);
    },
    buildGridColumns: function () {
        var me = this;
        
        me.columns = [
            {
                width: 50,
                xtype: 'widgetcolumn',
                locked: true,
                menuDisabled: true,
                widget: {
                    xtype: 'button',
                    textAlign: 'left',
                    arrowCls: '',
                    iconCls: 'fa fa-chevron-right',
                    handler: function (btn) {
                        var rec = btn.getWidgetRecord();
                        me.setSelection(rec);
                    }
                },
                onWidgetAttach: function (column, widget, record) {
                    var data = record.getData();
                    var menus = me.buildOpenForm(data);
                    widget.setMenu(menus);
                }
            }, {
                text: "WORKFLOW NAME",
                dataIndex: 'workflowName',
                width: 220,
                sortable: false,
                locked: true
            },{
                text: "REQUESTOR",
                flex: 1,
                sortable: true,
                menuDisabled: true,
                dataIndex: 'requestorName'
            }, {
                text: "LAST ACTION DATE",
                width: 150,
                sortable: true,
                renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                dataIndex: 'lastActionDate'
            }
        ];

        me.dockedItems = [{
            dock: 'top',
            xtype: 'toolbar',
            items: [{
                xtype: 'button',
                iconCls: 'fa fa-refresh',
                text: 'Refresh',
                handler: function () {
                    var store = me.getStore();
                    store.reload();
                }
            }]
        }];
        me.bbar = {
            xtype: 'pagingtoolbar',
            displayInfo: true
        };
    },
    
    buildOpenForm: function (data) {
        
        var me = this;

        var menus = [];
        var menu = null;


        return [{
            text: 'Open Form',
            iconCls: 'fa fa-folder-open',
            handler: function () {
                window.location.href = data.routeUrl;
            }
        }, {
            text: 'Delete Draft',
            iconCls: 'fa fa-remove',
            handler: function () {
                Ext.MessageBox.confirm('Confirm',
                    'Are you sure you want to delete draft?', function (btnText) {
                        if (btnText == 'yes') {
                            Ext.Ajax.request({
                                url: '/api/dashboard/tasks/delete-drafted',
                                method: 'POST',
                                jsonData: true,
                                params: {
                                    requestHeaderId: data.id
                                },
                                success: function () {
                                    me.getStore().reload();
                                }
                            });
                        }
                }, this);
                
            }
        }];
    }
});