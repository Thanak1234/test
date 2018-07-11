Ext.define('Workflow.view.ticket.setting.agent.AgentPanel',{
    extend: 'Ext.grid.Panel',
    xtype: 'ticket-setting-agent-panel',
    requires: [
        'Workflow.view.ticket.setting.agent.AgentPanelController',
        'Workflow.view.ticket.setting.agent.AgentPanelModel'
    ],
    controller: 'ticket-setting-agent-agent',
    viewModel: {
        type: 'ticket-setting-agent-agent'
    },
    scrollable: 'y',
    bind : {
        store: '{ticketSettingAgentStore}'
    },
    border: true,
    region: 'center',
    hideHeaders: false,
    title: 'Setting Agents',
    dockedItems: [{
        xtype: 'toolbar',
        dock: 'top',
        items: [
            {
                xtype: 'textfield',
                width: 300,
                bind: '{query}',
                emptyText: 'Search Agent, Group Policy, or Department...',
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
                handler: 'onAddHandler',
                iconCls: 'fa fa-plus-circle'
            }]
    }],
    height : 100,
    initComponent: function () {
        var me = this;        
        
        this.columns = [
            {xtype: 'rownumberer'},
            { 
                text: 'Emp No', dataIndex: 'employeeNo', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	}        
            },
            { 
                text: 'Emp Full Name', dataIndex: 'fullName', width: 150,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	}        
            },
            { text: 'Account Type', dataIndex: 'accountType', flex: 1 ,hidden: true,
                renderer : function(value, metadata, record) {
                   return me.showToolTip(value, metadata);
			 	} 
            },
            { text: 'Status', dataIndex: 'status', 
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
                },
                bind: {
                    width: '{columnStatusWidth}'
                }
            },
            { text: 'Group Policy', dataIndex: 'groupPolicyGroupName', flex: 1 },            
            { text: 'Department', dataIndex: 'deptName', flex: 1 ,
                renderer : function(value, metadata, record) {
                   return me.showToolTip(value, metadata);
			 	} 
            },
            { text: 'Description', dataIndex: 'description', flex: 2 ,
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
