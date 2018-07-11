Ext.define('Workflow.model.fnfForm.Reservation', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id', type: 'int' },
		{ name: 'requestHeaderId', type: 'int' },
		{ name: 'roomCategoryId', type: 'int' },
		{ name: 'requestHeader' },
		{ name: 'roomCategory' },
		{ name: 'guestFullName', type: 'string' },
		{ name: 'passportNo', type: 'string' },
		{ name: 'relationship', type: 'string' },
		{
		    name: 'extraBed',
		    type: 'bool'
		},
		{ name: 'numberOfRoom', type: 'int' },
		{ name: 'paxsAdult', type: 'int' },
		{ name: 'paxsChild', type: 'int' },
		{ name: 'remark', type: 'string' },
		{ name: 'agree', type: 'bool' },
		{ name: 'termConditionId', type: 'int' },
		{ name: 'confirmationNumber', type: 'string' },
		{
		    name: 'checkOutDate',
		    type: 'date'
		},
		{
		    name: 'checkInDate',
		    type: 'date'
		},
		{
		    name: 'receiveDate',
		    type: 'date'
		}
	],
	proxy: {
	    writer: { writeAllFields: true },
	    type: 'rest',
	    api: {
	        read: Workflow.global.Config.baseUrl + 'api/fnfrequest',
	        create: Workflow.global.Config.baseUrl + 'api/fnfrequest',
	        update: Workflow.global.Config.baseUrl + 'api/fnfrequest',
	        destroy: Workflow.global.Config.baseUrl + 'api/fnfrequest'
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
