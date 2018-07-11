Ext.define('Workflow.view.ticket.report.ReportViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-report-reportview',
    data: {
        form: {

        },
        disable: {
            assignee: false,
            subCate: false,
            item: false
        }
    },
    formulas: {
      isEdit: function(get){
          return false;
      }
    },

    stores: {
        ticketTypeStore: {
            model: 'Workflow.model.ticket.TicketType',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticketrpt/lookup?key=TYPE',
                reader: {
                    type: 'json',
                    rootProperty: 'records'
                }
            }
        },
        ticketStatusStore: {
            model: 'Workflow.model.ticket.TicketStatus',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticketrpt/lookupEx?key=STATUS',
                reader: {
                    type: 'json',
                    rootProperty: 'records'
                }
            }
        },

        ticketSourceStore: {
            model: 'Workflow.model.ticket.TicketSource',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticketrpt/lookup?key=SOURCE',
                reader: {
                    type: 'json',
                    rootProperty: 'records'
                }
            }
        },

        ticketTeamStore: {
            model: 'Workflow.model.ticket.TicketTeam',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticketrpt/lookup?key=GROUP',
                reader: {
                    type: 'json',
                    rootProperty: 'records'
                }
            }
        },

        ticketAgentStore: {
            model: 'Workflow.model.common.GeneralLookup',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticketrpt/agentsList',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }

        },

        ticketCateStore: {
            model: 'Workflow.model.ticket.TicketCate',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticketrpt/lookup?key=CATEGORY',
                reader: {
                    type: 'json',
                    rootProperty: 'records'
                }
            }
        },
        ticketSubCateStore: {
            model: 'Workflow.model.common.GeneralLookup',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/sub-cates',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },

        ticketItemStore: {
            model: 'Workflow.model.common.GeneralLookup',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/items',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        ticketPriorityStore: {
            model: 'Workflow.model.ticket.TicketPriority',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticketrpt/lookup?key=PRIORITY',
                reader: {
                    type: 'json',
                    rootProperty: 'records'
                }
            }
        },
        ticketSlaStore: {
            model: 'Workflow.model.common.GeneralLookup',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/getSlaFilter',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }

});
