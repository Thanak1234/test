Ext.define('Workflow.view.ticket.setting.sla.SlaMappingPanel', {
    extend: 'Ext.grid.Panel',
    xtype: 'ticket-setting-sla-slamappingpanel',
    requires: [
        'Workflow.view.ticket.setting.sla.SlaMappingPanelController',
        'Workflow.view.ticket.setting.sla.SlaMappingPanelModel'
    ],
    controller: 'ticket-setting-sla-slamappingpanelcontroller',
    viewModel: {
        type: 'ticket-setting-sla-slamappingpanelmodel'
    },
    scrollable: 'y',
    bind: {
        store: '{ticketSettingSlaMappingStore}'
    },
    border: true,
    region: 'center',
    hideHeaders: false,
    //title: 'SLA Mapping List',
    height: 100,
    initComponent: function () {
        var me = this;
        
        me.dockedItems = me.buildDockedItems();
        me.columns = me.buildColumns();
        me.callParent(arguments);
    },
    listeners: {
        itemdblclick: 'onDblClickHandler'
    },
    showToolTip: function (value, metadata) {
        value ? metadata.tdAttr = 'data-qtip="' + value + '"' : '';
        return value;
    },
    buildDockedItems: function () {
        var me = this;
        return [{
            xtype: 'toolbar',
            dock: 'top',
            items: [
                '->',
                {
                    text: 'Add Mapping',
                    xtype: 'button',
                    handler: 'onAddSlaMappingHandler',
                    iconCls: 'fa fa-plus-circle'
                }]
        }, {
                xtype: 'toolbar',
                dock: 'bottom',
                items: ['->', {
                    text: 'Close',
                    handler: 'onWindowClosedHandler',
                    iconCls: 'fa fa-times-circle-o'
                }]
        }];
    },
    buildColumns: function () {
        var me = this;
        return [
            { xtype: 'rownumberer' },            
            {
                text: 'Type', dataIndex: 'ticketType', flex: 1,
                renderer: function (value, metadata, record) {
                    return me.showToolTip(value, metadata);
                }
            },
            {
                text: 'Priority', dataIndex: 'priority', flex: 1,
                renderer: function (value, metadata, record) {
                    return me.showToolTip(value, metadata);
                }
            }, {
                text: 'SLA', dataIndex: 'sla', flex: 1,
                renderer: function (value, metadata, record) {
                    return me.showToolTip(value, metadata);
                }
            },
            {
                text: 'Description', dataIndex: 'description', flex: 1,
                renderer: function (value, metadata, record) {
                    return me.showToolTip(value, metadata);
                }
            },
            {
                xtype: 'actioncolumn',
                //cls: 'tasks-icon-column-header tasks-edit-column-header',
                width: 24,
                //iconCls: 'grid-edit-record',
                iconCls: 'fa fa-pencil-square-o',
                tooltip: 'Edit',
                sortable: false,
                handler: 'onEditHandler'
            }
        ];
    }
});
