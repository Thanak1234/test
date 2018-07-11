Ext.define('Workflow.model.common.worklists.OOF', {
    extend: 'Ext.data.Model',
    fields: [
        'ShareType',
		'EndDate',
		'Name',
		'ID'
    ],
    hasOne: [
        {
            name: 'WorkType',
            model: 'Workflow.model.common.worklists.WorkType',
            associationKey: 'WorkType'
        }
    ],
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/worklists/oofs',
        reader: {
            type: 'json'
        },
        noCache: false,
        limitParam: undefined,
        pageParam: undefined,
        startParam: undefined
    }
});