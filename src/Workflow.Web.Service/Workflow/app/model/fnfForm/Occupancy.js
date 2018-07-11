Ext.define('Workflow.model.fnfForm.Occupancy', {
	extend: 'Workflow.model.Base',
	fields: [
		{ name: 'id', type: 'int' },
        { name: 'occupancy', type: 'string' },
		{
		    name: 'checkDate',
		    type: 'date'
		}
	]
});
