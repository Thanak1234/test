Ext.define('Workflow.model.icdForm.incidentEmployee', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id' },
        { name: 'empno'},        
        { name: 'requestHeaderId', type: 'int'}
     
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
