Ext.define('Workflow.model.itRequestForm.RequestUser', {
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
        {name: 'hiredDate',     	type: 'date'	},
        {name: 'manager',     	type: 'string'	},
        {name: 'phone',     	type: 'string'	}
    ]
});
