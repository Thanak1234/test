Ext.define('Workflow.view.ticket.setting.grouppolicy.TicketGroupPolicyFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-grouppolicy-ticketgrouppolicyform',
    data: {
        form: {
            'id': null,
            'groupName': null,
            'status': null,
            'createTicket': null,
            'editTicket': null,
            'editRequestor': null,
            'postTicket': null,
            'closeTicket': null,
            'assignTicket': null,
            'mergeTicket': null,
            'deleteTicket': null,
            'deptTransfer': null,
            'changeStatus': null,
            'limitAccess': null,
            'newTicketNotify': null,
            'assignedNotify': null,
            'replyNotify': null,
            'changeStatusNotify': null,
            'description': null,
            'createdDate': null,
            'modifiedDate': null,
            subTicket : null,
            reportLimitAccess: null
        },
        customLimitAccessFocusValue: null,
        customReportLimitAccessFocusValue: null
    },
    formulas:{
        isEdit: function (get) {
            return  get('id')!=null;
        }
    },
    stores: {
        statusStore: {
            model: 'Workflow.model.ticket.TicketLookup'
        },
        limitAccessStore: {
            model: 'Workflow.model.ticket.TicketLookup'
        },
        reportLimitAccessStore: {
            model: 'Workflow.model.ticket.TicketLookup'
        },
        newTicketNotifyStore: {
           model: 'Workflow.model.ticket.TicketLookup'
        },
        assignedNotifyStore: {
            model: 'Workflow.model.ticket.TicketLookup'
        },
        replyNotifyStore: {
            model: 'Workflow.model.ticket.TicketLookup'
        },
        changeStatusNotifyStore: {
            model: 'Workflow.model.ticket.TicketLookup'
        },
        assignedTeamStore: {
             model: 'Workflow.model.ticket.TicketGroupPolicyTeams',
             autoLoad: false,
             proxy: {
                 type: 'rest',
                 url: Workflow.global.Config.baseUrl + 'api/ticket/setting/grouppolicy/team-list',
                 reader: {
                     type: 'json',
                     rootProperty: 'data',
                     totalProperty: 'totalCount'
                 }
             }
        },
        assignedReportLimitAccessTeamStore: {
            model: 'Workflow.model.ticket.TicketGroupPolicyTeams',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/grouppolicy/report-team-list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }

});
