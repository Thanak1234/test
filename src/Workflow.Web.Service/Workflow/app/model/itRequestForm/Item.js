Ext.define('Workflow.model.itRequestForm.Item', {
    extend: 'Workflow.model.Base',
	
    fields: [
        {name: 'id',     		type: 'int'		},
        {name: 'sessionId',		type: 'int'		},
        {name: 'itemName',     	type: 'string'	},
		{name: 'description',	type: 'string'		},
		{name: 'hodRequired'	}
    ]
});
