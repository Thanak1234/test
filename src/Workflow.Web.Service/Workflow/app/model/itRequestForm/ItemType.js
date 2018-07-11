Ext.define('Workflow.model.itRequestForm.ItemType', {
    extend: 'Workflow.model.Base',
	
    fields: [
        {name: 'id'				, type: 'int'	},
        {name: 'itemTypeName'	, type: 'string'},
        {name: 'description'	, type: 'string'}
    ]
});
