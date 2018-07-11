Ext.define('Workflow.model.ticket.TicketSettingSubCategory', {
    extend: 'Ext.data.Model',
    idProperty:  'id',
    fields: [
        'id',
        'cateId',
        'subCateName',
        'cateName',
        'cateDescription',
        'description',
        'createdDate',
        'modifiedDate',
        'status',
        'statusId'
    ],
    proxy: {

        type: 'rest',
        api: {
            update: 'api/ticket/setting/subcate/update',
            create: 'api/ticket/setting/subcate/create',
            destroy: 'api/ticket/setting/subcate/delete'
        },
        reader: {
            type: 'json',
            rootProperty: 'data',
            totalProperty: 'totalCount'
        }
    }
});
