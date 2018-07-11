Ext.define('Workflow.model.rights.UserRight', {
    extend: 'Ext.data.Model',
    fields: [
        'id',
        'roleRightId',
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
		'desc',
		{name: 'hiredDate',	type: 'date'},
        'darId',
        'createdDate',
        'active'
    ]
});