Ext.define('Workflow.model.common.RequestForm', {
    extend: 'Workflow.model.Base',
	
    fields: [
        {name: 'requestNo',     type: 'string'	},
        {name: 'action',     	type: 'string'	},
        {name: 'comment',     	type: 'string'	},
        {name: 'activity',     	type: 'string'	},
		{name: 'serial',     	type: 'string'	},
		{ name: 'priority', type: 'int' },
        { name: 'lastActivity' },
        { name: 'status' }
    ],
    proxy: {
        type: 'rest',
        url : Workflow.global.Config.baseUrl + 'api/itrequest'
    }
});
