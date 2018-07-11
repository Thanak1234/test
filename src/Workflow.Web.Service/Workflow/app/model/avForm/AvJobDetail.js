Ext.define('Workflow.model.avForm.AvJobDetail', {
    extend: 'Workflow.model.Base',
	
    fields: [
        {name: 'id'         ,   type: 'int'     },
        {name: 'projectName',	type: 'string'	},
        {name: 'location',     	type: 'string'	},
		{name: 'setupDate',		type: 'date'	},
		{ name: 'actualDate', type: 'date' },
        { name: 'projectBrief', type: 'string' },
        { name: 'other', type: 'string' }
    ]
});
