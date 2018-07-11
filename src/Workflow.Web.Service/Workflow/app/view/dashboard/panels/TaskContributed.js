Ext.define("Workflow.view.dashboard.panels.TaskContributed", {
    extend: "Ext.grid.Panel",
    xtype: 'dashboard-taskcontributed',
    requires: [
        'Ext.toolbar.Paging',
        'Ext.ux.form.SearchField'
    ],
    controller: "dashboard-taskcontributed",
    viewModel: {
        type: "dashboard-taskcontributed"
    },
    //title: 'All Task I Contributed',
    //iconCls : 'fa fa-tasks',
    stateful: false,
    //collapsible: true,
    //headerBorders: false,
    columnLines: true,
    multiColumnSort: false,
    loadMask: true,
    bind: {
        selection: '{selectedRow}'
    },
    viewConfig: {                
        listeners: {
            refresh: function (dataview) {
                Ext.each(dataview.panel.columns, function(column) {
                    if (column.autoSizeColumn === true)
                        column.autoSize();
                })
            }
        }
    },

    listeners: {
        rowdblclick: 'onOpenFormAction'
    },
    initComponent: function () {
        var me = this;
        var store = Ext.create('Workflow.store.dashboard.Tasks', {
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/dashboard/tasks/contributed',
                reader: {
                    type: 'json',
                    rootProperty: 'Records',
                    totalProperty: 'TotalRecords'
                },
                enablePaging: true
            }            
        });

        me.store = store;

        me.initColumns();
        me.initToolbars();
        me.initPagingToolbar();

        me.callParent(arguments);
    },
    loadData: function(){
        if (!this.isLoad) {
            this.store.load();
            this.isLoad = true;
        }
    },
    initColumns: function() {
        var me = this;

        function highlight (value) {
            var newValue = me.searchRegex ? value.toString().replace(me.searchRegex, '<span style="color:red;font-weight:bold; font-size:110%">$1</span>') : value;
            return newValue;
        }

        me.columns = [
            {
                xtype: 'rownumberer',
                hidden: true
            },
            {
                xtype: 'actioncolumn',
                width: 32,
                locked: true,
                items: [{
                    iconCls: 'fa fa-sitemap',
                    tooltip: 'View Flow',
                    scope: 'controller',
                    handler: 'onOpenWorkFlowHandler'
                }]
            },
            {
                text: "FOLIO", sortable: true, locked: true, menuDisabled: true, dataIndex: 'folio', width: 120,
                renderer: function (value) { return '<span style="color:blue;font-weight:bold; font-size:105%">' + value + '</span>'; }
            },
            { text: "REQUESTOR", sortable: true, locked: true, menuDisabled: true, dataIndex: 'requestor', width: 120, renderer: highlight },
            { text: "FORM NAME", sortable: true, menuDisabled: true, dataIndex: 'appName', autoSizeColumn: false, flex: 1, renderer: highlight },
            {
                text: "SUBMITTED DATE", sortable: true, menuDisabled: true, dataIndex: 'submittedDate', width: 130, renderer: function (value) {
                    var date = Ext.util.Format.date(value, 'Y-m-d H:i:s');
                    return highlight(date);
                }
            },
            { text: "LAST ACTIVITY", sortable: true, menuDisabled: true, dataIndex: 'lastActivity', width: 130, renderer: highlight },
            { text: "LAST ACTION BY", sortable: true, menuDisabled: true, dataIndex: 'lastActionBy', width: 130, renderer: highlight },
            {
                text: "LAST ACTION DATE", sortable: true, menuDisabled: true, dataIndex: 'lastActionDate', width: 130, renderer: function (value) {
                    var date = Ext.util.Format.date(value, 'Y-m-d H:i:s');
                    return highlight(date);
                }
            },
            { text: "ACTION", sortable: true, menuDisabled: true, dataIndex: 'status', width: 80, renderer: highlight }
        ];
    },
    initToolbars: function() {
        var me = this;
        me.dockedItems = [{
            dock: 'top',
            xtype: 'toolbar',
            items: [
                {
                    xtype: 'button',
                    text: 'Refresh',
                    iconCls: 'fa fa-refresh',
                    disabled: false,
                    handler: 'onRefreshClick'
                },
                {
                    xtype: 'button',
                    text: 'Audit Log',
                    iconCls: 'fa fa-eye',
                    disabled: false,
                    handler: 'onViewAuditClicked',
                    bind: {
                        disabled: '{!selectedRow}'
                    }
                },
                '->',
                {
                    xtype: 'textfield',
                    width: 275,
                    emptyText: 'Search',
                    reference: 'searchTextBox',
                    enableKeyEvents: true,
                    bind: {
                        value: '{searchText}'
                    },
                    triggers: {
                        clear: {
                            cls: 'x-form-clear-trigger',
                            handler: 'onClearClickHandler',
                            hidden: true
                        },
                        search: {
                            cls: 'x-form-search-trigger',
                            weight: 1,
                            handler: 'onSearchClickHandler'
                        }
                    },
                    listeners: {
                        change: 'onSearchChangeHandler',                        
                        keypress: 'onSearchKeypressHandler',
                        buffer: 0
                    }
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