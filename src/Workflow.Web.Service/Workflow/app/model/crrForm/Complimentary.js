Ext.define('Workflow.model.crrForm.Complimentary', {
	extend: 'Workflow.model.Base',
	
	fields: [
		{ name: 'id', type: 'int' },
        { name: 'roomCategoryId', type: 'int' },
        { name: 'vipStatusId', type: 'int' },
        { name: 'purposeId', type: 'int' },
        
        { name: 'arrivalFlightDetail', type: 'string' },
        { name: 'arrivalTransfer', type: 'bool' },
        { name: 'arrivalVehicleTypeId', type: 'int' },
        { name: 'departureFlightDetail', type: 'string' },
        { name: 'departureTransfer', type: 'bool' },
        { name: 'departureVehicleTypeId', type: 'int' },
        { name: 'roomCharge', type: 'bool' },
        { name: 'room', type: 'int' },
        { name: 'adult', type: 'int' },
        { name: 'childrent', type: 'int' },
        { name: 'extraBed', type: 'bool' },
        { name: 'specialRequest', type: 'string' },
        { name: 'remark', type: 'string' },
        { name: 'departmentIncharge', type: 'string' },
        { name: 'confirmationNumber', type: 'string' },
        { name: 'arrivalDate', type: 'date'},
        { name: 'departureDate', type: 'date' },
        { name: 'RoomSubCategoryId', type: 'int' }
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
