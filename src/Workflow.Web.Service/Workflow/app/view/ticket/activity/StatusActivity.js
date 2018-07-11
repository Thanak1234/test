/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.StatusActivity',{
    extend: 'Workflow.view.ticket.activity.Activity',

    requires: [
        'Workflow.view.ticket.activity.StatusActivityController',
        'Workflow.view.ticket.activity.StatusActivityModel'
    ],

    controller: 'ticket-activity-statusactivity',
    viewModel: {
        type: 'ticket-activity-statusactivity'
    },

    getMainItems: function () {
        return [
          
           {
              xtype : 'grid',
              border: true,
              margin: '0 0 10 0' ,
              bind: {
                store: '{subTicketStore}',
                hidden: '{!showSubtickets}'
              },
              columns: [
                {
                  xtype: 'rownumberer'
                },
                {
                  text: 'Subticket #',
                  width: 100,
                  sortable: true,
                  dataIndex: 'ticketNo'
                },
                {
                  text: 'Subject',
                  flex: 1,
                  sortable: true,
                  dataIndex: 'subject'
                },
                {
                  text: 'Created Date',
                  width: 130,
                  sortable: true,
                  dataIndex: 'createdDate',
                  renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s')
                },
                {
                  text: 'Status',
                  width: 70,
                  sortable: true,
                  dataIndex: 'status'
                }
              ]
           },
            {
             xtype: 'combo',
             labelWidth: 100,
             editable: false,
             queryMode:  'local',
             fieldLabel: 'Status',
             reference:  'status',
             displayField: 'status',
             allowBlank: false,
             forceSelection: true,
             valueField: 'id',
             width: 400,
             bind: {
               store: '{ticketStatusStore}',
               selection: '{statusVal}'
             }
           }, {
             xtype: 'fieldset',
             layout: 'hbox',
             title: 'Actual Time',
             bind : {
               hidden : '{!actualHoursShown}'
             },
             width: '95%',
             items:[
               {
                   fieldLabel: 'Hours',
                   labelAlign: 'right',
                   labelWidth: 100,
                   xtype: 'numberfield',
                   minValue : 0,
                   width: 300,
                   bind:{
                        value: '{actualHours}' //,
                      //  hidden : '{!actualHoursShown}'
                   }
               },
               {
                   fieldLabel: 'Minutes',
                   labelAlign: 'right',
                   labelWidth: 100,
                   xtype: 'numberfield',
                   minValue : 0,
                   width: 300,
                   listeners: {
                     blur : 'onMinuteCalcHandler'
                   },
                   bind:{
                        value: '{actualMinutes}'
                   }
               }
             ]
            }, {
                xtype: 'ngLookup',
                labelWidth: 100,
                editable: true,
                allowBlank: false,
                fieldLabel: 'Root Cause',
                reference: 'rootCause',
                displayField: 'cause',
                allowBlank: false,
                forceSelection: true,
                valueField: 'causeId',
                url: 'api/ticket/lookup/root-causes',
                width: '95%',
                hidden: true,
                bind: {
                    value: '{rootCauseId}',
                    hidden: '{rootCauseHidden}',
                    readOnly: '{readonly}',
                    disabled: '{rootCauseHidden}'
                }
            },
          {
            xtype: 'checkbox',
            bind:{
                  hidden : '{!k2Integration}',
                  boxLabel : '{k2IntegrationMsg}',
                  disabled : '{!canTriggerK2Action}',
                  value : '{closeK2Form}',
                  hidden : '{!k2IntegrationOption}'
             }
          }


        ];
    }
});
