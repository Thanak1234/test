Ext.define('Workflow.view.ticket.setting.sla.SlaPanel',{
    extend: 'Ext.grid.Panel',
    xtype: 'ticket-setting-sla-panel',
    requires: [
        'Workflow.view.ticket.setting.sla.SlaPanelController',
        'Workflow.view.ticket.setting.sla.SlaPanelModel'
    ],
    controller: 'ticket-setting-sla-sla',
    viewModel: {
        type: 'ticket-setting-sla-sla'
    },
    scrollable: 'y',
    bind : {
        store: '{ticketSettingSlaStore}'
    },
    border: true,
    region: 'center',
    hideHeaders: false,
    title: 'Setting SLA',
    dockedItems: [{
        xtype: 'toolbar',
        dock: 'top',
        items: [
            {
                xtype: 'textfield',
                width: 300,
                bind: '{query}',
                emptyText: 'Search Sla, Priority...',
                triggers: {
                    clear: {
                        cls: 'x-form-clear-trigger',
                        handler: 'onNavFilterClearTriggerClick',
                        hidden: true,
                        scope: 'controller'
                    },
                    search: {
                        cls: 'x-form-search-trigger',
                        weight: 1,
                        handler: 'onFilterHandler',
                        scope: 'controller'
                    }
                },

                listeners: {
                    specialkey: 'onEnterFilterHandler',
                    change: 'onNavFilterChanged'
                }
            },
            '->',
            {
                text: 'SLA Mapping',
                xtype: 'button',
                handler: 'onSlaMappingHandler',
                iconCls: 'fa fa-map-signs'
            },
            {
                text: 'Add',
                xtype: 'button',
                handler: 'onAddHander',
                iconCls: 'fa fa-plus-circle'
            }]
    }],
    height : 100,
    initComponent: function () {
        var me = this;        
        
        this.columns = [
            {xtype: 'rownumberer'},
            { 
                text: 'SLA Name', dataIndex: 'slaName', flex: 1,                
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	} 
        
            },            
            { text: 'Description', dataIndex: 'description', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	} 
            },
            { 
                text: 'Resolution Time', 
                dataIndex: 'gracePeriod', 
                flex: 1,
                renderer: 'renderDateStr'
            },
            { text: 'Response Time', dataIndex: 'firstResponsePeriod', flex: 2 ,
                renderer: 'renderDateStr' 
            },           
            {
                text: 'Created Date', dataIndex: 'createdDate',
                renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                bind: {
                    width: '{columnDateWidth}'
                }
            },
            {
                text: 'Modified Date', dataIndex: 'modifiedDate',
                renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                bind: {
                    width: '{columnDateWidth}'
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

        this.callParent(arguments);
    },
    listeners: {
        itemdblclick: 'onDblClickHandler'
    },
    showToolTip: function(value, metadata){
        value ? metadata.tdAttr = 'data-qtip="' + value + '"' : '';
        return value;
    }
});
