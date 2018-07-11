Ext.define('Workflow.store.ticket.Report', {
    extend: 'Ext.data.Store',
    alias: 'store.ticketReport',
    model: 'Workflow.model.ticket.TicketReport',
    pageSize: 15,    
    proxy: {
        type: 'rest',
        url: 'api/ticketrpt/search',
        reader: {
            type: 'json',
            rootProperty: 'records',
            totalProperty: 'totalRecords'
        },
        enablePaging: true
    }
});