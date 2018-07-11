/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.MergedActivityModel', {
    extend: 'Workflow.view.ticket.activity.ActivityModel',
    alias: 'viewmodel.ticket-activity-mergedactivity',
    data: {
        toTicket : null,
        requiredComment : false
    },
    stores: {
        ticketListingStore: {
            model: 'Workflow.model.ticket.TicketListing',
            pageSize: 0,
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
    }

});
