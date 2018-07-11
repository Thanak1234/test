Ext.define('Workflow.model.mtfForm.Treatment', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: "id", type: "int" },
        { name: "requestHeaderId", type: "int" },
        { name: "projectName", type: "string" },
        { name: "businessUnit", type: "string" },
        { name: "projectLead", type: "string" },
        { name: "accountManager", type: "string" },
        { name: "budget", type: "float" },
        { name: "billingInfo", type: "string" },
        { name: "submissionDate", type: "date" },
        { name: "requiredDate", type: "date" },
        { name: "actualEventDate", type: "date" },
        { name: "introduction", type: "string" },
        { name: "targetMarket", type: "string" },
        { name: "usage", type: "string" },
        { name: "briefing", type: "string" },
        { name: "designDuration", type: "string" },
        { name: "productDuration", type: "string" },
        { name: "dateline", type: "date" },
        { name: "brainStorm", type: "date" },
        { name: "projectStart", type: "date" },
        { name: "firstRevision", type: "date" },
        { name: "secondRevision", type: "date" },
        { name: "finalApproval", type: "date" },
        { name: "completion", type: "date" },
        { name: "projectReference", type: "string" },
        { name: "draftComment", type: "string" }
	],
	proxy: {
	    writer: { writeAllFields: true },
	    type: 'rest',
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
