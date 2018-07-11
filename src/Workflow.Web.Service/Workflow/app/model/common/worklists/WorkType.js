Ext.define('Workflow.model.common.worklists.WorkType', {
    extend: 'Ext.data.Model',
    fields: [
		'Name',
		'ID'
    ],
    hasMany: [
        {
            name: 'WorkTypeExceptions',
            model: 'Workflow.model.common.worklists.WorkTypeException',
            associationKey: 'WorkTypeExceptions'
        }
    ],    
    hasMany: [
        {
            name: 'Destinations',
            model: 'Workflow.model.common.worklists.Destination',
            associationKey: 'Destinations'
        }
    ],
    hasMany: [
        {
            name: 'WorklistCriterias',
            model: 'Workflow.model.common.worklists.WorklistCriteria',
            associationKey: 'WorklistCriterias'
        }
    ]
});