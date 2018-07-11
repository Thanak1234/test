Ext.define('Workflow.model.common.ActivityHistory', {
    extend: 'Workflow.model.Base',
	
    fields: [
        {name: 'id',                    type: 'int'		},
        {name: 'activity',              type: 'string'	},
        {name: 'actionDate',            type: 'date'	},
		{name: 'approver',              type: 'string'	},
		{name: 'approverDisplayName',   type: 'string'	},
		{name: 'decision',     	        type: 'string'	},
		{name: 'comment',     	        type: 'string'	}
    ]
    
});
