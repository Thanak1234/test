Ext.define('Workflow.model.ticket.TicketTeam', {
    extend: 'Ext.data.Model',
    idProperty: 'id',
    fields: [
        'id',
        'teamName',
        'status',
        'statusId',
        'alertAllMembers',
        'alertAssignedAgent',
        'directoryListing',        
        'createdDate',
        'modifiedDate',
        'description',
        'agentId',
        'immediateAssign',
        'registeredAgents',
        'immediateAssignAgentId'
    ],
    proxy: {
        
        type : 'rest',
        api : {
            update: 'api/ticket/setting/team/update',
            create : 'api/ticket/setting/team/create', 
            destroy: 'api/ticket/setting/team/delete'
        },
        actionMethods : {
			update : 'POST'
		},
        reader :{
                type : 'json',
                rootProperty: 'data',
                totalProperty: 'totalCount'
        }
    }
});
