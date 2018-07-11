Ext.define("Workflow.view.roles.RoleNavigation", {
    extend: "Ext.tree.Panel",
    xtype: 'roles-navigation',
    viewModel: {
        type: "roles-navigation"
    },
    title: 'Navigation',
    rootVisible: false,
    lines: true,
    useArrows: true,
    hideHeaders: true,
    collapseFirst: false,
    width: 300,
    minWidth: 100,
    split: true,
    stateful: true,
    collapsible: true,
    store: Ext.create('Ext.data.TreeStore', {
        autoLoad: true,
        proxy: {
            type: 'ajax',
            url: Workflow.global.Config.baseUrl + 'api/roles/treeitems'
        }
    }),
    initComponent: function () {
        var me = this;
        me.buildColumns();
        me.buildItems();
        me.buildDockItems();
        me.buildColumns();
        
        me.listeners = {
            itemcontextmenu: function (view, record, item, index, e) {
                e.stopEvent();
                Ext.create('Ext.menu.Menu', {
                    width: 140,
                    items: me.buildContextMenu(record)
                }).showAt(e.getXY());
            }
        };

        me.callParent(arguments);
    },
    //afterRender: function(){
    //    var me = this;
    //    this.getStore().load({
    //        callback: function(){
    //            me.refresh();
    //        }
    //    });
    //    this.callParent(arguments);
    //},
    buildItems: function () {
        var me = this;
    },
    buildColumns: function () {
        var me = this;
        me.columns = [{
            xtype: 'treecolumn',
            flex: 1,
            dataIndex: 'text',
            scope: 'controller',
            renderer: 'treeNodeRenderer'
        }];
    },
    buildDockItems: function () {
        var me = this;
        me.dockedItems = [
            {
                xtype: 'toolbar',
                dock: 'top',
                padding: 0,
                style: {
                    backgroundColor: '#ECECEC'
                },
                defaults: {
                    margin: 3
                },
                items: [
                    {
                        xtype: 'textfield',
                        emptyText: 'Search',
                        reference: 'searchTextField',
                        flex: 1,
                        triggers: {
                            clear: {
                                cls: 'x-form-clear-trigger',
                                handler: 'onNavigationFilterClearClick',
                                hidden: true
                            },
                            search: {
                                cls: 'x-form-search-trigger',
                                weight: 1,
                                handler: 'onNavigationFilterSearchClick'
                            }

                        },
                        listeners: {
                            change: 'onNavigationFilterChangeHandler',
                            buffer: 300
                        }
                    },
                    {
                        xtype: 'button',
                        iconCls: 'fa fa-refresh',
                        width: 50,
                        listeners: {
                            click: 'onTreeRefreshClick'
                        }
                    }
                ]
            }
        ];
    },
    listeners: {
        selectionchange: 'onSelectionChangeHandler'
    },
    buildContextMenu: function (rec) {
        return [{
            text: 'Edit',
            iconCls: 'fa fa-pencil-square-o',
            listeners: {
                click: function () {
                    var process = Ext.create('Workflow.model.Process');
                    console.log('process', process);
                    Ext.create('Workflow.view.roles.EditApplication', {
                        viewModel: {
                            data: {
                                submitBtText: 'Save',
                                workflow: Ext.create('Workflow.model.Process').getData()
                            }
                        }
                    }).show();
                }
            }
        }];
    }
});