Ext.define('Workflow.model.erfForm.Requisition', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id', type: 'string' },
        { name: 'requestHeaderId', type: 'int' },
        { name: 'referenceNo', type: 'string' },
        { name: 'position', type: 'string' },
        { name: 'reportingTo', type: 'string' },
        { name: 'salaryRange', type: 'string' },
        { name: 'requestTypeId', type: 'int' },
        { name: 'shiftTypeId', type: 'int' },
        { name: 'locationTypeId', type: 'int' },
        { name: 'private', type: 'bool' },
        { name: 'requisitionNumber', type: 'string' },
        { name: 'justification', type: 'string' }
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
