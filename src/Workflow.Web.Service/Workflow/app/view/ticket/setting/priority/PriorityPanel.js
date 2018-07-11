Ext.define('Workflow.view.ticket.setting.priority.PriorityPanel',{
    extend: 'Ext.grid.Panel',
    xtype: 'ticket-setting-priority-panel',
    requires: [
        'Workflow.view.ticket.setting.priority.PriorityPanelController',
        'Workflow.view.ticket.setting.priority.PriorityPanelModel'
    ],
    controller: 'ticket-setting-priority-priority',
    viewModel: {
        type: 'ticket-setting-priority-priority'
    },
    scrollable: 'y',
    bind : {
        store: '{ticketSettingPriorityStore}'
    },
    border: true,
    region: 'center',
    hideHeaders: false,
    title: 'Setting category',
    dockedItems: [{
        xtype: 'toolbar',
        dock: 'top',
        items: [
            {
                xtype: 'textfield',
                width: 300,
                bind: '{query}',
                emptyText: 'Search here...',
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
                text: 'Priority', dataIndex: 'priorityName', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	} 
        
            },            
            { text: 'Description', dataIndex: 'description', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	} 
            },{ 
                text: 'SLA', dataIndex: 'slaName', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	} 
        
            },            
            { text: 'SLA Description', dataIndex: 'slaDescription', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	} 
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
