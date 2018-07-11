/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.MergedActivityController', {
    extend: 'Workflow.view.ticket.activity.ActivityController',
    alias: 'controller.ticket-activity-mergedactivity',

    onTicketChanged: function (queryPlan, eOpts) {
        var vm = this.getView().getViewModel();
        var store = vm.getStore('ticketListingStore');
        if (store) {
            Ext.apply(store.getProxy().extraParams, {
                keyword:  queryPlan.query,
                execptTecktId: vm.get('ticket').getId()
            });
        }
    },
    getMoreData: function () {
        var me = this;
        var vm = me.getView().getViewModel();
        return {
            mergedToTkId: vm.get('toTicket')?vm.get('toTicket').getId() : null
        }
    },

    validate: function(){
       var validates = [], data = this.getMoreData();
        validates.push({ propName: 'Ticket to be merged', prop: 'mergedToTkId' ,isRequired: true})
        try {
            this.validation(data, validates);
            return true;    
        } catch (err) {

            Ext.MessageBox.alert({
                title: 'Dat validation',
                msg: err,
                icon:Ext.MessageBox.ERROR,
                buttons: Ext.MessageBox.YES
            });

            return false;
        }
        
    }
    
});
