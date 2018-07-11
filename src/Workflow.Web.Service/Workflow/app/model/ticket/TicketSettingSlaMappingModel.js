Ext.define('Workflow.model.ticket.TicketSettingSlaMappingModel', {
    extend: 'Ext.data.Model',
    idProperty: 'id',
    fields: [
        'id',        
        'ticketType',
        'priority',
        'sla',
        'typeId',
        'priorityId',
        'slaId'
    ],
    proxy: {

        type: 'rest',
        api: {
            update: 'api/ticket/setting/sla/mapping/create',
            create: 'api/ticket/setting/sla/mapping/create',
            destroy: 'api/ticket/setting/sla/mapping/delete'
        },
        reader: {
            type: 'json',
            rootProperty: 'data',
            totalProperty: 'totalCount'
        }
    }
});
