Ext.define('Workflow.model.itRequestForm.ItemRole', {
    extend: 'Workflow.model.Base',
	
    fields: [
        {name: 'id'				, type: 'int'	},
        {name: 'roleName'	, type: 'string'},
        {name: 'description'	, type: 'string'},
		{name: 'isAdmin'}
    ]
});
