Ext.define('Workflow.model.fatForm.AssetTransferDetail', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id', type: 'int' },
        { name: 'requestHeaderId', type: 'int' },
		{ name: 'transferCode', type: 'string' },
		{ name: 'transferDate' },
		{ name: 'description', type: 'string' }
	]
});