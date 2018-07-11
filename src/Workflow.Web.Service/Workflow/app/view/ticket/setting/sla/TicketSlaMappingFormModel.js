Ext.define('Workflow.view.ticket.setting.sla.TicketSlaMappingFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-sla-ticketslamappingformmodel',
    data: {                        
        form: {
            'id': null,                        
            'description': null,
            'typeId': null,
            'slaId': null,
            'priorityId': null
        }
    },
    formulas:{        
        isEdit: function (get) {
            return  get('id')!=null;
        }
    },
    stores: {        
        ticketTypeStore: {
            model: 'Workflow.model.ticket.TicketType',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/ticket_type',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        ticketPriorityStore: {
            model: 'Workflow.model.ticket.TicketPriority',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/priority/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        ticketSlaStore: {
            model: 'Workflow.model.ticket.TicketSla',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/sla/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }

});
