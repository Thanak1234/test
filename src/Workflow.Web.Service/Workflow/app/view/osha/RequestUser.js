Ext.define('Workflow.model.osha.RequestUser', {
    extend: 'Workflow.model.Base',
	
    fields: [
        {name: 'id',     		type: 'int'		},
        {name: 'empId',         type: 'int'	},
        {name: 'empNo',     	type: 'string'	},
		{name: 'empName',     	type: 'string'	},
		{name: 'teamId',     	type: 'int'	    },
		{name: 'teamName',     	type: 'string'	},
		{name: 'position',     	type: 'string'	},
		{name: 'email',     	type: 'string'	},
        {name: 'manager',     	type: 'string'	},
        { name: 'phone', type: 'string' },
        { name: 'empType', type: 'string' }
    ]
});
