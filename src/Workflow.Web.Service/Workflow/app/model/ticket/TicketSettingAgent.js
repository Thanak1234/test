Ext.define('Workflow.model.ticket.TicketSettingAgent', {
    extend: 'Ext.data.Model',
    idProperty: 'id',
    fields: [
        'id',        
        'accountTypeId',
        'accountType',
        'statusId',
        'status',
        'groupPolicyGroupName',
        'groupPolicyId',
        'deptName',
        'deptId',
        'description',
        'createdDate',
        'modifiedDate',
        'teamId',
        'immediateAssign',
        'empId',
        'employeeNo',
		'fullName',
        'position',
		'subDeptId',
		'subDept',
		'groupName',
		'devision',
		'phone',
		'ext',
		'email',
		'hod',
		'priority',
		'reportTo',        
		{name: 'hiredDate',	type: 'date'}
    ],
    proxy: {

        type: 'rest',
        api: {
            update: 'api/ticket/setting/agent/update',
            create: 'api/ticket/setting/agent/create',
            destroy: 'api/ticket/setting/agent/delete'
        },
        reader: {
            type: 'json',
            rootProperty: 'data',
            totalProperty: 'totalCount'
        }
    }
});
