Ext.define('Workflow.model.ticket.TicketGroupPolicy', {
    extend: 'Ext.data.Model',
    idProperty: 'id',
    fields: [
        'id',
        'groupName',
        'status',
        'statusId',
        'createTicket',
        'editTicket',
        'editRequestor',
        'postTicket',
        'closeTicket',
        'assignTicket',
        'mergeTicket',
        'deleteTicket',
        'deptTransfer',
        'changeStatus',
        'limitAccess',
        'limitAccessId',
        'newTicketNotify',
        'newTicketNotifyId',
        'assignedNotify',
        'assignedNotifyId',
        'replyNotify',
        'replyNotifyId',
        'changeStatusNotify',
        'changeStatusNotifyId',
        'description',
        'createdDate',
        'modifiedDate',
        'reportAccess',
        'reportAccessId'
    ],
    proxy: {

        type : 'rest',
        api : {
            update: 'api/ticket/setting/grouppolicy/update',
            create : 'api/ticket/setting/grouppolicy/create',
            destroy: 'api/ticket/setting/grouppolicy/delete'
        },
        reader :{
                type : 'json',
                rootProperty: 'data',
                totalProperty: 'totalCount'
        }
    }
});
