Ext.define('Workflow.model.mcForm.incidentEmployee', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id' },
        { name: 'empno'},        
        { name: 'request_Header_Id', type: 'int'}
     
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
