Ext.define('Workflow.model.crrForm.Guest', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id' },
        { name: 'name'},
        { name: 'title'},
        { name: 'companyName' },
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
