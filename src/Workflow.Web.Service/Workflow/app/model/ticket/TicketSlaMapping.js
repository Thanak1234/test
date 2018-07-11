Ext.define('Workflow.model.ticket.TicketSlaMapping', {
    extend: 'Ext.data.Model',
    fields: [
        'id',
        'slaId',
        'typeId',        
        'priorityId',
        'description'
    ],
    proxy: {        
        type : 'rest',
        api : {
            update: 'api/ticket/setting/sla/mapping/update',
            create: 'api/ticket/setting/sla/mapping/create', 
            destroy: 'api/ticket/setting/sla/mapping/delete'
        },
        reader :{
                type : 'json',
                rootProperty: 'data',
                totalProperty: 'totalCount'
        }
    }
});
