Ext.define('Workflow.model.bcjForm.RequestItem', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id', type: 'int' },
		//{ name: 'projectDetailId', type: 'int' },
		//{ name: 'projectDetail' },
		{ name: 'item', type: 'string' },
		{ name: 'unitPrice', type: 'float' },
		{ name: 'quantity', type: 'float' },
		{ name: 'total', type: 'float' }
	]
});
