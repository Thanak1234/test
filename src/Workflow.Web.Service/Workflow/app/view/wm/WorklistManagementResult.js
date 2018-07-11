Ext.define('Workflow.view.wm.WorklistManagementResult', {
    extend: 'Ext.grid.Panel',
    xtype: 'wm-result',
    title: 'Worklist',
    frame: true,
    viewConfig: {
        listeners: {
            refresh: function (dataview) {
                Ext.each(dataview.panel.columns, function (column) {
                    if(column.autoResize == true)
                        column.autoSize();
                })
            }
        }
    },
    initComponent: function() {
        var me = this;

        var store = Ext.create('Workflow.store.dashboard.Tasks', {
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/wms/byCriteria',
                reader: {
                    type: 'json',
                    rootProperty: 'Records',
                    totalProperty: 'TotalRecords'
                },
                enablePaging: true
            }
        });

        me.store = store;

        me.buildColumns();
        me.buildButtons();
        me.initPagingToolbar();
        me.callParent(arguments);
    },
    buildColumns: function () {
        var me = this;
        me.columns = [
            {
                text: "FORM", flex: 1, sortable: true, menuDisabled: true, autoResize: true, flex: 1, dataIndex: 'procName'
            },
            {
                text: "ACTIVITY NAME", flex: 1, sortable: true, menuDisabled: true, flex: 1, dataIndex: 'activityName'
            },
            {
                text: "EVENT NAME", flex: 1, sortable: true, menuDisabled: true, flex: 1, dataIndex: 'eventName'
            },
            {
                text: "FOLIO", flex: 1, sortable: true, menuDisabled: true, flex: 1, dataIndex: 'folio'
            },
            {
                text: "DESTINATION", flex: 1, sortable: true, menuDisabled: true, dataIndex: 'displayName', flex: 1
            },
            {
                text: "WORKLIST DATE", flex: 1, sortable: true, menuDisabled: true, dataIndex: 'startDate', flex: 1, renderer: function (value) {
                    return Ext.util.Format.date(value, 'Y-m-d H:i:s');
                }
            },
            {
                text: "STATUS", flex: 1, sortable: true, menuDisabled: true, dataIndex: 'status', flex: 1,
                renderer: function (val) {
                    var status = '';
                    switch (val) {
                        case 0:
                            status = '<span style="color:green">Available</span>';
                            break;
                        case 1:
                            status = '<span style="color:blue">Open</span>';
                            break;
                        case 2:
                            status = '<span style="color:#FE9A2E">Allocated</span>';
                            break;
                        case 3:
                            status = '<span style="color:red">Sleep</span>';
                            break;
                    }
                    return status;
                }
            }
        ];
    },
    buildButtons: function () {
        var me = this;

        me.dockedItems = [{
            dock: 'top',
            xtype: 'toolbar',
            items: [
                {
                    xtype: 'button',
                    text: 'Share',
                    iconCls: 'fa fa-share-alt-square',
                    disabled: false,
                    handler: 'onShareClick'
                },
                {
                    xtype: 'button',
                    text: 'Redirect',
                    iconCls: 'fa fa-user',
                    disabled: false,
                    handler: 'onRedirectClick'
                },
                {
                    xtype: 'button',
                    text: 'Release',
                    iconCls: 'fa fa-unlock-alt',
                    disabled: false,
                    handler: 'onReleaseClick'
                }
            ]
        }];
    },
    initPagingToolbar: function () {
        var me = this;
        me.bbar = Ext.create('Ext.PagingToolbar', {
            store: me.store,
            displayInfo: true,
            displayMsg: 'Displaying tasks {0} - {1} of {2}',
            emptyMsg: "No tasks to display",
            listeners: {

            }
        });
    }
});