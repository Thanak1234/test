/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.MergedActivity',{
    extend: 'Workflow.view.ticket.activity.Activity',

    requires: [
        'Workflow.view.ticket.activity.MergedActivityController',
        'Workflow.view.ticket.activity.MergedActivityModel'
    ],

    controller: 'ticket-activity-mergedactivity',
    viewModel: {
        type: 'ticket-activity-mergedactivity'
    },

    getMainItems: function () {
        return [{
            xtype: 'combo',
            width: '100%',
            fieldLabel: 'To Ticket',
            typeAhead: true,
            displayField: 'ticketNo',
            typeAheadDelay: 100,
            minChars: 2,
            valueField: 'id',
            queryMode: 'remote',
            forceSelection: true,
            emptyText: 'ticket number or subject',
            bind: {
                store: '{ticketListingStore}',
                selection: '{toTicket}'
            },
            listConfig: {
                minWidth: 500,
                resizable: true,
                loadingText: 'Searching...',
                emptyText: 'No matching posts found.',
                itemSelector: '.search-item',
                itemTpl: [
                    '<a >',
                        '<h3><span>{subject}</span>(#{ticketNo})</h3>',
                        '<i style="line-height:1.4;border-left:2px solid #009688;margin-left:20px">Ticket Status: <strong>{status}</strong></i>',
                    '</a>'
                ]
            },
            triggers: {
                clear: {
                    weight: 1,
                    cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                    hidden: true,
                    //handler: 'onClearClick',
                    scope: 'this'
                }
            },
            listeners: {
                //change: 'onEmplyeePickupChanged',
                //expand: 'onEmplyeePickupChanged'//'onEmplyeeExpandCombobox'
                beforequery: 'onTicketChanged'
            }
        }];
    }
});
