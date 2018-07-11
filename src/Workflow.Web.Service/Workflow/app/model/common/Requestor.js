Ext.define('Workflow.model.common.Requestor', {
    extend: 'Workflow.model.Base',
	
    fields: [
        {name: 'id',     		type: 'int'		},
        {name: 'employeeNo',    type: 'string'	},
        {name: 'fullName',     	type: 'string'	},
		{name: 'position',     	type: 'string'	},
		{name: 'subDept',     	type: 'string'	},
		{name: 'group',     	type: 'string'	},
		{name: 'devision',     	type: 'string'	},
		{name: 'priority',     	type: 'string'	}
    ]
});
