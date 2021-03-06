Ext.define('Workflow.model.attForm.NAGATravel', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id', type: 'int' },
        { name: 'classTravelEntitlement', type: 'int' },
        { name: 'classTravelRequest', type: 'int' },
        { name: 'reasonException', type: 'string' },
        { name: 'purposeTravel', type: 'string' },
        { name: 'estimatedCostTicket', type: 'float' },
        { name: 'requestHeaderId', type: 'int' }
        
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
