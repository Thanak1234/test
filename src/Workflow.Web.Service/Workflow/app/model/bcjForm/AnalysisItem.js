Ext.define('Workflow.model.bcjForm.AnalysisItem', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id', type: 'int' },
		//{ name: 'projectDetailId', type: 'int' },
		//{ name: 'projectDetail' },
		{ name: 'description', type: 'string' },
		{ name: 'revenue', type: 'float' },
		{ name: 'quantity', type: 'float' },
		{ name: 'total', type: 'float' }
	]
});
