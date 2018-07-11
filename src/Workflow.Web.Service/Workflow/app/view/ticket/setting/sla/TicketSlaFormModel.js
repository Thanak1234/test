Ext.define('Workflow.view.ticket.setting.sla.TicketSlaFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-sla-ticketslaform',
    data: {                        
        form: {
            'id': null,            
            'slaName': null,            
            'description': null,
            'createdDate': null,
            'modifiedDate': null,
            'numDayResolution': null,
            'numHourResolution': null,
            'numMinuteResolution': null,
            'numDayResponse': null,
            'numHourResponse': null,
            'numMinuteResponse': null            
        },
        minNumDayResolution: 0,
        minNumHourResolution: 0,
        minNumMinuteResolution: 0,
        maxNumDayResolution: 99,
        maxNumHourResolution: 23,
        maxNumMinuteResolution: 59,
        minNumDayResponse: 0,
        minNumHourResponse: 0,
        minNumMinuteResponse: 0,
        maxNumDayResponse: 99,
        maxNumHourResponse: 23,
        maxNumMinuteResponse: 59
    },
    formulas:{        
        isEdit: function (get) {
            return  get('id')!=null;
        }
    },
    stores: {        
        dayStoreResolution: Ext.create('Ext.data.Store', {
            fields: ['id', 'num']
        }),
        hourStoreResolution: Ext.create('Ext.data.Store', {
            fields: ['id', 'num']
        }),
        minuteStoreResolution: Ext.create('Ext.data.Store', {
            fields: ['id', 'num']
        }),
        dayStoreResponse: Ext.create('Ext.data.Store', {
            fields: ['id', 'num']
        }),
        hourStoreResponse: Ext.create('Ext.data.Store', {
            fields: ['id', 'num']
        }),
        minuteStoreResponse: Ext.create('Ext.data.Store', {
            fields: ['id', 'num']
        })
    }

});
