Ext.define('Workflow.model.ticket.Ticket', {
    //extend: 'Ext.data.Model',
    extend: 'Workflow.model.Base',
    fields: [
        { name: 'id', type: 'int' },
        { name: 'ticketNo', type: 'string' },
        { name: 'subject', type: 'string' }
    
    ],
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/ticket/loadTicket'
    }
});
