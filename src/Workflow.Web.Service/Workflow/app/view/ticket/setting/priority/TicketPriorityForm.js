
Ext.define("Workflow.view.ticket.setting.priority.TicketPriorityForm",{
    extend: "Ext.panel.Panel",

    requires: [
        "Workflow.view.ticket.setting.priority.TicketPriorityFormController",
        "Workflow.view.ticket.setting.priority.TicketPriorityFormModel"
    ],

    controller: "ticket-setting-priority-ticketpriorityform",
    viewModel: {
        type: "ticket-setting-priority-ticketpriorityform"
    },
    scrollable : 'y',
    frame: true,
    layout: {
        type: 'anchor',
        pack: 'start',
        align: 'stretch'
    },
    
    initComponent: function () {
        var me = this;        
        me.buttons = [
            {
                xtype: 'button', align: 'right',
                text: 'Save',
                handler: 'onFormSubmit',
                iconCls: 'fa fa-save'
            },
            { xtype: 'button', align: 'right', text: 'Close', handler: 'onWindowClosedHandler', iconCls: 'fa fa-times-circle-o'}
        ];

        me.items = me.buildItems();

        me.callParent(arguments);
    },
    buildItems: function () {
        return [{
            xtype: 'form',
            reference: 'formRef',
            anchor: '100%',            
            border: true,
            bodyPadding: '10 10 0',
            margin: 5,
            items: [{
                xtype: 'textfield',                
                anchor: '100%',
                allowBlank: false,
                fieldLabel: 'Name',
                bind: { 
                    value: '{form.priorityName}',
                    readOnly: '{isEdit}' 
                }
            },{
                fieldLabel: 'SLA',
                allowBlank: false,
                anchor: '100%',
                xtype: 'combo',
                forceSelection: true,
                queryMode: 'local',
                displayField: 'slaName',                
                valueField: 'id',
                listeners: {                    
                    scope: 'controller'                    
                },
                bind: {
                    selection: '{form.sla}',
                    store: '{ticketSettingSlaStore}',
                    readOnly: '{isEdit}'
                }
            },{
                xtype: 'textarea',
                anchor: '100%',
                fieldLabel: 'Description',
                bind: { value: '{form.description}' }
            }]
        }];
    }

});
