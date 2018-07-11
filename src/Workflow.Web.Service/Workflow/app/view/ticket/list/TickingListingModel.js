/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.list.TickingListingModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-list-tickinglisting',
    data: {
        name: 'Workflow',
        classify: 'myOpenTickets'
    },

    formulas: {
        classifySelection: function (get) {

        }
    },
    stores: {
        ticketListingStore : {
            model: 'Workflow.model.ticket.TicketListing'
        }
    }

});
