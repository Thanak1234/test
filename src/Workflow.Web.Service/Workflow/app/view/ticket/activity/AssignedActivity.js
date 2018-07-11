/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.AssignedActivity',{
    extend: 'Workflow.view.ticket.activity.Activity',

    requires: [
        'Workflow.view.ticket.activity.AssignedActivityController',
         'Workflow.view.ticket.activity.AssignedActivityModel'
    ],

    controller: 'ticket-activity-assignedactivity',
    viewModel: {
        type: 'ticket-activity-assignedactivity'
    },

    getMainItems: function () {
        return [
           {
               fieldLabel: 'Group',
               xtype: 'combo',
               forceSelection: true,
               queryMode: 'local',
               displayField: 'teamName',
               reference: 'ticketTeamRef',
               allowBlank: false,
               valueField: 'id',
               width: 400,
               bind: {
                   store: '{ticketTeamStore}',
                   selection   : '{team}' 
               },
               listeners: {
                   change: 'onTeamChanged',
                   scope: 'controller'
               }
           }, {
               fieldLabel: 'Assignee',
               width: 400,
               xtype: 'combo',
               reference: 'ticketAgentRef',
               bind: {
                   store: '{ticketAgentStore}',
                   selection: '{agent}'
               },
               displayField: 'display',
               queryMode: 'local',
               //forceSelection: true,
               minChars: 2,
               listConfig: {
                   minWidth: 500,
                   resizable: true,
                   loadingText: 'Searching...',
                   itemSelector: '.search-item',
                   itemTpl: [
                       '<a >',
                           '<h3><span>{display}</span>({display1})</h3>',
                           '{display2}',
                       '</a>'
                   ]
               }

           }
        ];
    }
});
