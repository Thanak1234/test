Ext.define('Workflow.model.ticket.TicketSettingCategory', {
    extend: 'Ext.data.Model',
    idProperty: 'id',
    fields: [
        'id',
        'deptId',
        'cateName',
        'description',
        'createdDate',
        'modifiedDate',
        'status',
        'statusId'
    ],
    proxy: {

        type: 'rest',
        api: {
            update: 'api/ticket/setting/cate/update',
            create: 'api/ticket/setting/cate/create',
            destroy: 'api/ticket/setting/cate/delete'
        },
        reader: {
            type: 'json',
            rootProperty: 'data',
            totalProperty: 'totalCount'
        }
    }
});
