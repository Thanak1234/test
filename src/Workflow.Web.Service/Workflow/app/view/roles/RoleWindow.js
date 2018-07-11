Ext.define("Workflow.view.roles.RoleWindow", {
    extend: "Ext.window.Window",
    xtype: 'roles-rolewindow',
    controller: "roles-rolewindow",
    viewModel: {
        type: "roles-rolewindow"
    },
    title: 'User Roles',
    frame: true,
    width: 800,
    height: 400,    
    layout: {
        type: 'vbox',
        pack: 'start',
        align: 'stretch'
    },
    modal: true,
    maximizable: true,
    extraParam: null,

    initComponent: function () {
        var me = this;

        me.buildItems();
        me.buildButtons();

        me.callParent(arguments);
    },
    buildItems: function() {
        var me = this;

        var store = Ext.create('Workflow.store.roles.Roles');

        Ext.apply(store.getProxy().extraParams, me.extraParam);        

        me.items = [
            {
                xtype: 'grid',
                flex: 1,
                width: '100%',
                height: '100%',
                reference: 'grid',
                viewConfig: {
                    listeners: {
                        refresh: function (dataview) {
                            Ext.each(dataview.panel.columns, function (column) {
                                column.autoSize();                                    
                            })
                        }
                    }
                },
                selModel: {
                    selType: 'checkboxmodel'
                },
                tbar: [
                    '->',
                    {
                        xtype: 'button',
                        text: 'Remove',
                        iconCls: 'fa fa-times',
                        bind: {
                            disabled: '{!grid.selection}'
                        },
                        handler: 'onRemoveClick'
                    },
                    {
                        xtype: 'button',
                        text: 'Refresh',
                        iconCls: 'fa fa-refresh',
                        handler: 'onRefreshClick'
                    }
                ],
                store: store,
                columns: [
                    {
                        xtype: 'rownumberer'
                    },
                    {
                        text: 'Workflow',
                        sortable: true,
                        isAutoSize: true,
                        dataIndex: 'workflow'
                    },
                    {
                        text: 'Activity',
                        sortable: true,
                        isAutoSize: true,
                        dataIndex: 'activity'
                    },
                    {
                        text: 'Role',
                        sortable: true,
                        dataIndex: 'fullName'
                    },
                    {
                        text: 'Type',
                        sortable: true,
                        dataIndex: 'type'
                    }
                ]
            }
        ];
        store.load();
    },
    buildButtons: function () {
        var me = this;

        me.buttons = [
            {
                text: 'Ok',
                handler: 'onOkClick'
            }                        
        ];
    }
});
