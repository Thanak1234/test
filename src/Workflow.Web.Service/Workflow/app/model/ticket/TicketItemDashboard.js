Ext.define('Workflow.model.ticket.TicketItemDashboard', {
    extend: 'Workflow.model.Base',
    fields: [
        { name: 'id', type: 'int' },
        { name: 'itemName', type: 'string' },
        { name: 'opened', type: 'int' },
        { name: 'closed', type: 'int' },
        { name: 'onHold', type: 'int' },
        { name: 'overDue',type: 'int' },
        { name: 'merged', type: 'int' },
        { name: 'unAssigned', type: 'int' },
        { name: 'deleted',type: 'int' }
    ]
});