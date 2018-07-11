Ext.define('Workflow.model.ticket.TicketPriority', {
    extend: 'Ext.data.Model',
    fields: [
        'id',
        'priorityName',
        'description',
        'createdDate',
        'modifiedDate',
        'slaId',
        'slaName',
        'slaDescription'
    ],
    proxy: {
        
        type : 'rest',
        api : {
            update: 'api/ticket/setting/priority/update',
            create : 'api/ticket/setting/priority/create', 
            destroy: 'api/ticket/setting/priority/delete'
        },        
        reader :{
                type : 'json',
                rootProperty: 'data',
                totalProperty: 'totalCount'
        }
    }
});
