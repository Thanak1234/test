Ext.define('Workflow.model.ticket.TicketDepartment', {
    extend: 'Ext.data.Model',
    idProperty: 'id',
    fields: [
        'id',
        'deptName',
        'hodId',
        'internalUse',
        'automationEmail',
        'deptSignature',
        'defaultItemId',
        'isDefault',
        'status',
        'description',
        'createdDate',
        'modifiedDate',
        'status',
        'statusId'
    ],
    proxy: {

        type : 'rest',
        api : {
            update: 'api/ticket/setting/dept/update',
            create : 'api/ticket/setting/dept/create',
            destroy: 'api/ticket/setting/dept/delete'
        },
        reader :{
                type : 'json',
                rootProperty: 'data',
                totalProperty: 'totalCount'
        }
    }
});
