Ext.define('Workflow.model.ticket.TicketStatus', {
    extend: 'Ext.data.Model',
    fields: [
        'id',
        'status',
         { name: 'stateId', type: 'int' }
    ]
});
