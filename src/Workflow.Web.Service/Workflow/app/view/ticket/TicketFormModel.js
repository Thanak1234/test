/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.TicketFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-ticketform',
    data: {
        createdBy: null,
        ticket: null,
        form: {
            requestor: null,
            priority: null,
            subject : null,
            description: null,
            ticketType: null,
            impact: null,
            urgency: null,
            team: null,
            status: null,
            source : null,
            assignee: null,
            category: null,
            subCate: null,
            createdDate: null,
            item: null,
            site: null,
            comment: null,
            sla: null
        },
        data: null,
        isTicketTicketTypeChangeInit: false,
        isTicketTicketPriorityChangeInit: false
    },

    formulas:{
        createdByAgent: function (get) {
            var createBy = get('createdBy');
            if (createBy.toUpperCase() === 'AGENT') {
                return true;
            }else{
                return false;
            }
        },
        isEdit: function (get) {
            return  get('ticket')!=null;
        },

        required: function (get) {
            return get('createdBy').toUpperCase() === 'AGENT' || get('ticketId') != null;
        },
        renderSlaValue: function (get) {
            console.log('adsfadf 111', get('sla'));
            return true;
        }
    },

    stores: {
        ticketTypeStore : {
            model: 'Workflow.model.ticket.TicketType',
            autoLoad: false
        },
        ticketStatusStore: {
            model: 'Workflow.model.ticket.TicketStatus'
        },

        ticketSourceStore: {
            model: 'Workflow.model.ticket.TicketSource'
        },

        ticketImpactStore : {
            model: 'Workflow.model.ticket.TicketImpact'
        },

        ticketUrgencyStore : {
            model: 'Workflow.model.ticket.TicketUrgency'
        },

        ticketPriorityStore : {
            model: 'Workflow.model.ticket.TicketPriority',
            autoLoad: false
        },

        ticketSiteStore : {
            model: 'Workflow.model.ticket.TicketSite'
        },
        
        ticketTeamStore: {
            model: 'Workflow.model.ticket.TicketTeam'
        },

        ticketAgentStore:{
            model: 'Workflow.model.common.GeneralLookup',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/agent-list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }

        },

        ticketCateStore:{
            model: 'Workflow.model.ticket.TicketCate'
        },
        ticketSubCateStore: {
            model: 'Workflow.model.common.GeneralLookup',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/sub-cate',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },

        ticketItemStore:{
            model: 'Workflow.model.common.GeneralLookup',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/item',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },

        userUploadFilesStore: {
            model: 'Workflow.model.common.SimpleFileUpload'
        },
        agentUploadedFilesStore: {
            model: 'Workflow.model.common.SimpleFileUpload'
        }
    }

});
