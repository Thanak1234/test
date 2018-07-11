Ext.define('Workflow.view.ticket.setting.grouppolicy.GroupPolicyPanel',{
    extend: 'Ext.grid.Panel',
    xtype: 'ticket-setting-grouppolicy-panel',
    requires: [
        'Workflow.view.ticket.setting.grouppolicy.GroupPolicyPanelController',
        'Workflow.view.ticket.setting.grouppolicy.GroupPolicyPanelModel'
    ],
    controller: 'ticket-setting-grouppolicy-grouppolicy',
    viewModel: {
        type: 'ticket-setting-grouppolicy-grouppolicy'
    },
    scrollable: 'y',
    bind : {
        store: '{ticketSettingGroupPolicyStore}'
    },
    border: true,
    region: 'center',
    hideHeaders: false,
    title: 'Setting Group Policy',
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
                text: 'Group Name', dataIndex: 'groupName', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	}

            }
            // ,{
            //     text: 'Create Ticket', dataIndex: 'createTicket', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Edit Ticket', dataIndex: 'editTicket', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Edit Requestor', dataIndex: 'editRequestor', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Post Ticket', dataIndex: 'postTicket', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Close Ticket', dataIndex: 'closeTicket', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Merge Ticket', dataIndex: 'mergeTicket', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Delete Ticket', dataIndex: 'deleteTicket', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Department Transfer', dataIndex: 'deptTransfer', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Change Status', dataIndex: 'changeStatus', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Limit Access', dataIndex: 'limitAccess', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'New Ticket Notify', dataIndex: 'newTicketNotify', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Assigned Notify', dataIndex: 'assignedNotify', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Reply Notify', dataIndex: 'replyNotify', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // },{
            //     text: 'Change Status Notify', dataIndex: 'changeStatusNotify', flex: 1,
            //     renderer : function(value, metadata, record) {
            //         return me.showToolTip(value, metadata);
			//  	}

            // }
            ,{
                text: 'Status', dataIndex: 'status',
                bind: {
                    width: '{columnStatusWidth}'
                } 
			 	    } ,
            { text: 'Description', dataIndex: 'description', flex: 1,
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
