Ext.define('Workflow.model.ticket.TicketListing', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'id', type: 'int' },
		{ name: 'ticketNo', type: 'string' },
		{ name: 'subject', type: 'string' },
		{ name: 'requestor', type: 'string' },
        { name: 'assignee', type: 'string' },
        {
            name: 'createdDate',
            type: 'date'
        },
        { name: 'status', type: 'string' },
        { name: 'state', type: 'string' },
        { name: 'priority', type: 'string' },
        { name: 'teamName', type: 'string' },
        { name: 'lastAction', type: 'string' },
        { name: 'lastActionDate', type: 'date', dateFormat: 'c' },
        { name: 'lastActionBy', type: 'string' },
        { name: 'waitActionedBy', type: 'string' },
        { name: 'reminder', type: 'date', dateFormat: 'c' },
        { name: 'sla', type: 'string' },
        { name: 'source', type: 'string' },
        { name: 'ticketType', type: 'string' },
        { name: 'ticketTypeIcon', type: 'string'}
    ]
});
