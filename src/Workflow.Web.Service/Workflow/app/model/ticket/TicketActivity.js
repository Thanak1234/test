Ext.define('Workflow.model.ticket.TicketActivity', {
    extend: 'Workflow.model.Base',
    fields: [
        { name: 'id', type: 'int' },
        { name: 'activityType', type: 'string' },
        { name: 'action', type: 'string' },
        { name: 'actionBy', type: 'string' },
        { name: 'description', type: 'string' },
        { name: 'createdDate', type: 'date' },
        { name: 'modifiedDate', type: 'date' },
        { name: 'version', type: 'int' }
    ]
});