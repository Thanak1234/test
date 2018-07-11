Ext.define('Workflow.model.itRequestForm.RequestItem', {
    extend: 'Workflow.model.Base',
	
    fields: [
        {name: 'id',     		type: 'int'		},
        {name: 'itemId',		type: 'int'		},
        {name: 'sessionId',       type: 'int'	},
        {name: 'session',       type: 'string'	},
        {name: 'itemName',     	type: 'string'	},
		{name: 'itemTypeId',	type: 'int'		},
		{name: 'itemTypeName',	type: 'string'	},
		{name: 'itemRoleId',	type: 'int'		},
		{name: 'itemRoleName',	type: 'string'	},
		{name: 'qty',     		type: 'int'		},
        {name: 'comment',     	type: 'string'	}
    ]
});
