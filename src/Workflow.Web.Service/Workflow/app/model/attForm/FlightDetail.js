Ext.define('Workflow.model.attForm.FlightDetail', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id' },
        { name: 'fromDestination' },
        { name: 'toDestination' },
        {
            name: 'date', type: 'date'
            //, dateFormat: "yyyy-MM-dd"

        },
        {
            name: 'time', type: 'date'
            //, dateFormat: "HH:mm"
        },
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
