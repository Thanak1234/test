Ext.define('Workflow.model.common.worklists.WorklistCriteria', {
    extend: 'Ext.data.Model',
    fields: [
        'ManagedUser',
		'Platform',
		'NoData',
		'Count',
		'StartIndex'
    ],
    hasMany: [
        {
            name: 'Filters',
            model: 'Workflow.model.common.worklists.Filter',
            associationKey: 'Filters'
        }
    ]
});