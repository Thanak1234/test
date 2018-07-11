Ext.define('Workflow.model.pbfForm.Specification', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id', type: 'int' },
        { name: 'itemId', type: 'int' },
		{ name: 'name', type: 'string' },
		{ name: 'description', type: 'string' },
		{ name: 'quantity', type: 'int' },
		{ name: 'itemRef', type: 'string' }
	]
});