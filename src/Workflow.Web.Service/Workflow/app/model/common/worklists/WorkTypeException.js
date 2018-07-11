Ext.define('Workflow.model.common.worklists.WorkTypeException', {
    extend: 'Ext.data.Model',
    fields: [
		'Name',
		'Id',
		'Process',
        'ProcessPath',
		'Activity',
        'ActDisplayName'
    ],
    hasMany: [
        {
            name: 'Destinations',
            model: 'Workflow.model.common.worklists.Destination',
            associationKey: 'Destinations'
        }
    ]
});