/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.StatusActivityModel', {
    extend: 'Workflow.view.ticket.activity.ActivityModel',
    alias: 'viewmodel.ticket-activity-statusactivity',
    data: {
        statusVal: null,
        actualHours: null,
        actualMinutes: null,
        hasSubtickets: false,
        requiredComment : true,
        k2IntegratedData: null,
        k2IntegrationMsg: null,
        closeK2Form: true,
        rootCause: null
    },

    stores: {
        ticketStatusStore: {
            model: 'Workflow.model.ticket.TicketStatus',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/ticket_status',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        subTicketStore : {
            model : 'Workflow.model.ticket.TicketListing'
        },
        ticketRootCauseStore: {
            model: 'Workflow.model.ticket.TicketLookup',
            autoLoad: false
        }
    },

    formulas:{
        actualHoursShown: function (get) {
            var status = get('statusVal');
            return status? status.get('stateId') !==1 : false;
        },
        showSubtickets : function(get){
            return get('hasSubtickets');
        },
        k2Integration : function(get){
          return get('k2IntegratedData')!=null;  
        },
        k2IntegrationOption : function(get){
            var status = get('statusVal');
            var closeStatus =  status? status.get('stateId') !==1 : false;
            return get('k2IntegrationMsg')!=null && closeStatus ;
        },
        rootCauseHidden: function (get) {
            var m = get('ticket');
            var status = get('statusVal');
            var type = m.get('type') || m.get('ticketType');
            return status ? (status.get('stateId') !== 2 || type != 'Incident') : false;
        }
    }

});
