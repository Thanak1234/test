/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.view.TicketDisplayModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-view-ticketdisplay',
    data: {
        acitvityType: null
    },

    stores: {
        activityStore: {
            model: 'Workflow.model.ticket.TicketActivity'
        },

        activityTypeStore : {
            model: 'Workflow.model.common.GeneralLookup'//,
            //isFiltered : true
        }
    },
    formulas : {
        actualTime : function(get){
            var minutes = get('ticket.actualMinutes') || 0;

            if(minutes ==0){
              return '';
            }

            var hours = Math.floor(minutes/60);

            minutes = minutes%60;

            return Ext.String.format('{0}{1}h {2}{3}m',hours>10?'' : '0', hours,   minutes>10? '' : '0', minutes );
        },

        firstResponseDate : function(get){
            var d = get('ticket.firstResponseDate');
            if(d){
                return Ext.util.Format.date(d, 'd/m/Y H:i:s'); 
            }else{
                return '';
            }
        },

        dueDate : function(get){
            var d = get('ticket.dueDate');
            if(d){
                return Ext.util.Format.date(d, 'd/m/Y H:i:s'); 
            }else{
                return '';
            }
        },
        finishedDate : function(get){
            var d = get('ticket.finishedDate');
            if(d){
                return Ext.util.Format.date(d, 'd/m/Y H:i:s'); 
            }else{
                return '';
            }
        },

        createdDate : function(get){
            var d = get('ticket.createdDate');
            if(d){
                return Ext.util.Format.date(d, 'd/m/Y H:i:s'); 
            }else{
                return '';
            }
        },
        hidden: function (get) {
            var source = get('ticket.source');
            if (source === 'Email')
                return false;
            else
                return true;
        }

    }
});
