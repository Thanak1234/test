Ext.define('Workflow.model.common.Department', {
    extend: 'Workflow.model.Base',
	
    fields: [
        'id',
        'deptId',
        'groupId',
		'teamCode',
		'teamName',
        'deptCode',
        'groupCode',
        'groupName',
        'deptType',
        'fullName'
    ]
});
