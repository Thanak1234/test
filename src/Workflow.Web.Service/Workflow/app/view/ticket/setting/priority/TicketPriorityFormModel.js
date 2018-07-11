Ext.define('Workflow.view.ticket.setting.priority.TicketPriorityFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-priority-ticketpriorityform',
    data: {                        
        form: {
            'id': null,            
            'priorityName': null,            
            'description': null,
            'createdDate': null,
            'modifiedDate': null,
            'sla': null            
        }
    },
    formulas:{        
        isEdit: function (get) {
            return  get('id')!=null;
        }
    },
    stores: {       
       ticketSettingSlaStore: {
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
