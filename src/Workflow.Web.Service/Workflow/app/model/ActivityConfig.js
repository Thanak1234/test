Ext.define('Workflow.model.ActivityConfig', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id', type: 'int' },
        { name: 'requestCode', type: 'string' },
        { name: 'activity' },
		{ name: 'actions'}
	]
});