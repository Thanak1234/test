Ext.define('Workflow.model.ticket.TicketLookup', {
    extend: 'Ext.data.Model',
    idProperty: 'id',
    fields: [
        'id',
        'display',
        'display1',
        'display2',
        'description'
    ],    
    proxy: {        
        type : 'rest',        
        url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/ticket-list',      
        reader :{
                type : 'json',
                rootProperty: 'data',
                totalProperty: 'totalCount'
        }
    }
});
