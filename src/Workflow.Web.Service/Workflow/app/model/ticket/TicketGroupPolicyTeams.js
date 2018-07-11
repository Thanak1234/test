Ext.define('Workflow.model.ticket.TicketGroupPolicyTeams', {
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
        'immediateAssignAgentId',
        'groupPolicyId'
    ]
});
