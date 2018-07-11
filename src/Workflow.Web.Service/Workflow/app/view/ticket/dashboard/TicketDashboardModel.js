/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.dashboard.TicketDashboardModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-dashboard-ticketdashboard',
    data: {

    },

    stores: {
        ticketItemStore : {
            model: 'Workflow.model.ticket.TicketItemDashboard',
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/dashboard/item-overview' ,
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }
});
