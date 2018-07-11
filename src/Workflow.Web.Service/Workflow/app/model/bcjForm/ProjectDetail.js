Ext.define('Workflow.model.bcjForm.ProjectDetail', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id', type: 'int' },
		{ name: 'requestHeaderId', type: 'int' },
		{ name: 'capexCategoryId', type: 'int' },
		{ name: 'requestHeader' },
		{ name: 'capexCategory' },
		{ name: 'projectName', type: 'string' },
		{ name: 'reference', type: 'string' },
		{ name: 'coporationBranch', type: 'string' },
		{ name: 'whatToDo', type: 'string' },
		{ name: 'whyToDo', type: 'string' },
		{ name: 'arrangement', type: 'string' },
		{ name: 'totalAmount', type: 'float' },
		{ name: 'estimateCapex', type: 'float' },
		{ name: 'incrementalNetContribution', type: 'float' },
		{ name: 'incrementalNetEbita', type: 'float' },
		{ name: 'paybackYear', type: 'float' },
		{ name: 'outlineBenefit', type: 'string' },
		{ name: 'showPurchasingPanel', type: 'bool' },
		{
		    name: 'commencement',
		    type: 'date'
		},
		{
		    name: 'completion',
		    type: 'date'
		},
		{ name: 'alternative', type: 'string' },
		{ name: 'outlineRisk', type: 'string' },
		{ name: 'capitalRequired', type: 'string' }
	],
	proxy: {
	    writer: { writeAllFields: true },
	    type: 'rest',
	    api: {
	        read: Workflow.global.Config.baseUrl + 'api/bcjrequest',
	        create: Workflow.global.Config.baseUrl + 'api/bcjrequest',
	        update: Workflow.global.Config.baseUrl + 'api/bcjrequest',
	        destroy: Workflow.global.Config.baseUrl + 'api/bcjrequest'
	    },
	    actionMethods: {
	        read: 'GET',
	        destroy: 'DELETE',
	        update: 'PUT',
	        create: 'POST'
	    },
	    reader: {
	        type: 'json',
	        rootProperty: 'Result',
	        totalProperty: 'TotalRecords'
	    }
	}
});
