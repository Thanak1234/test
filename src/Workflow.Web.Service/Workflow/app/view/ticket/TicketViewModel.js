/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.TicketViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-ticketview',
    data: {
        keyword: null,
        statusVal: null,
        initData: null,
        autoRefresh: false,
        columnDateWidth: 135,
        columnStatusWidth: 75
    },

    stores: {
        ticketStatusStore: {
            model: 'Workflow.model.ticket.TicketStatus',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/ticket_status',
                extraParams: {isFillter: true},
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },

        ticketListingStore: {
            model: 'Workflow.model.ticket.TicketListing',
            pageSize: 100,
            remoteSort: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/listing',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    },
    formulas: {
        isAgent: function (get) {
            var data=  get('initData');
            if (data && data.isAgent) {
                return true;
            } else {
                return false;
            }
        },

        autoRefreshIcon : function(get){
            var autoRefresh=  get('autoRefresh');
            if(autoRefresh){
                return 'fa fa-pause';
            }else{
                return 'fa fa-play';
                
            }
        }
    }

});
