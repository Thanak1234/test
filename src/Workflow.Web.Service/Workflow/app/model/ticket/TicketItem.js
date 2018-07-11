Ext.define('Workflow.model.ticket.TicketItem', {
    extend: 'Ext.data.Model',
    idProperty: 'id',
    fields: [
        'id',
        'subCateId',
        'itemName',
        'teamId',
        'slaId',
        'description',
        'createdDate',
        'modifiedDate',
        'itemDisplayName',
        'status',
        'statusId'
    ],
    proxy: {

        type : 'rest',
        api : {
            update: 'api/ticket/setting/item/update',
            create : 'api/ticket/setting/item/create',
            destroy: 'api/ticket/setting/item/delete'
        },
        reader :{
                type : 'json',
                rootProperty: 'data',
                totalProperty: 'totalCount'
        }
    }
});
