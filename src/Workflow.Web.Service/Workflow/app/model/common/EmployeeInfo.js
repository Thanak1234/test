/* global Ext */
Ext.define('Workflow.model.common.EmployeeInfo', {
    extend: 'Workflow.model.Base',

    fields: [
        'id',
        'employeeNo',
		'fullName',
        'position',
		'subDeptId',
		'subDept',
		'groupName',
        'deptName',
		'devision',
		'phone',
		'ext',
		'email',
		'hod',
		'priority',
		'reportTo',
        'isAdmin',
		{name: 'hiredDate',	type: 'date'}
    ]
});
