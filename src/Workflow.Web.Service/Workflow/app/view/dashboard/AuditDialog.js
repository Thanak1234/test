Ext.define('Workflow.view.dashboard.AuditDialog', {
    extend: 'Ext.window.Window',
    xtype: 'audit-dialog',
    controller: 'audit-dialog',
    viewModel: {
        type: 'audit-dialog'
    },
    title: 'Audit Log',
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

        var store = Ext.create('Workflow.store.common.InstanceAudit');

        Ext.apply(store.getProxy().extraParams, me.extraParam);        

        me.items = [
            {
                xtype: 'grid',
                flex: 1,
                width: '100%',
                height: '100%',
                reference: 'grid',
                tbar: [
                    '->',
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
                        text: 'Process Name',
                        sortable: true,
                        isAutoSize: true,
                        dataIndex: 'procName',
                        hidden: true
                    },
                    {
                        text: 'Date',
                        width: 140,
                        sortable: true,
                        dataIndex: 'date',
                        renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s')
                    },
                    {
                        text: 'Activity',
                        sortable: true,
                        isAutoSize: true,
                        dataIndex: 'actName',
                        hidden: false
                    },
                    {
                        text: 'Folio',
                        sortable: true,
                        dataIndex: 'folio',
                        hidden: true
                    },
                    {
                        text: 'User',
                        sortable: true,
                        dataIndex: 'user',
                        hidden: true
                    },
                    
                    {
                        text: 'Audit Description',
                        sortable: true,
                        dataIndex: 'auditDesc',
                        flex: 1
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
