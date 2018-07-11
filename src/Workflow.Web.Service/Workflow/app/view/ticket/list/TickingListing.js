/**
*@author : Phanny
*/
Ext.define("Workflow.view.ticket.list.TickingListing", {
    extend: "Ext.grid.Panel",
    xtype: 'ticket-listing',

    requires: [
        "Workflow.view.ticket.list.TickingListingController",
        "Workflow.view.ticket.list.TickingListingModel"
    ],

    controller: "ticket-list-tickinglisting",
    viewModel: {
        type: "ticket-list-tickinglisting"
    },
    title: 'Tickets',
   
    initComponent: function () {
        var me = this;
        this.columns = me.gridCols();
        me.callParent(arguments);
    },


    gridCols : function(){
        return [{
            xtype: 'rownumberer'
        }, {
            text: 'Ticket#',
            width: 100,
            sortable: true,
            dataIndex: 'ticketNo'
        }, {
            text: 'Subject',
            flex: 1,
            sortable: true,
            dataIndex: 'subject'
        }, {
            text: 'Created Date',
            width: 100,
            sortable: true,
            dataIndex: 'createdDate',
            renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s')
        }, {
            text: 'Requestor',
            width: 100,
            sortable: true,
            dataIndex: 'requestor'
        }, {
            text: 'Assigned To',
            width: 100,
            sortable: true,
            dataIndex: 'assignee'
        }, {
            text: 'Status',
            width: 100,
            sortable: true,
            dataIndex: 'status'
        }, {
            text: 'Priority',
            width: 100,
            sortable: true,
            dataIndex: 'priority'
        },
        {
            text: 'Wait Actioned By',
            width: 100,
            sortable: true,
            dataIndex: 'waitActionedBy'
        }, {
            xtype: 'ticketActionCol',
            dataIndex: 'id',
            cls: 'tasks-icon-column-header tasks-reminder-column-header',
            width: 24,
            tooltip: 'Set Reminder',
            menuPosition: 'tr-br',
            menuDisabled: true,
            sortable: false,
            emptyCellText: '',
            scope: 'controller',
            listeners: {
                select: 'onTicketActionHandler'
            }
        }];
    }
});
