Ext.define('Workflow.model.avForm.RequestItem', {
    extend: 'Workflow.model.Base',
	
    fields: [
        {name: 'id',            type: 'int'     },
        {name: 'itemId',		type: 'int'		},
        {name: 'itemName',     	type: 'string'	},
		{name: 'itemTypeId',	type: 'int'		},
		{ name: 'itemTypeName', type: 'string' },
        { name: 'itemTypeDescription', type: 'string' },
        { name: 'quantity', type: 'int' },
		{name: 'comment', 	    type: 'string' 	}
    ]
});
