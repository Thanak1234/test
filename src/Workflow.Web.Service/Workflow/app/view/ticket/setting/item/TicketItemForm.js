Ext.define("Workflow.view.ticket.setting.item.TicketItemForm",{
    extend: "Ext.panel.Panel",

    requires: [
        "Workflow.view.ticket.setting.item.TicketItemFormController",
        "Workflow.view.ticket.setting.item.TicketItemFormModel"
    ],

    controller: "ticket-setting-item-ticketitemform",
    viewModel: {
        type: "ticket-setting-item-ticketitemform"
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
                fieldLabel: 'Item Name',
                bind: {
                    value: '{form.itemName}',
                    readOnly: '{isEdit}'
                }
            }, {
                xtype: 'textarea',
               anchor: '100%',
                fieldLabel: 'Description',
                bind: { value: '{form.description}' }
            }, {
                fieldLabel: 'Team',
                allowBlank: false,
                anchor: '100%',
                xtype: 'combo',
                forceSelection: true,
                queryMode: 'local',
                displayField: 'teamName',
                valueField: 'id',
                listeners: {
                    scope: 'controller'
                },
                bind: {
                    selection: '{form.team}',
                    store: '{ticketTeamStore}',
                    readOnly: '{isEdit}'
                }
            }, {
                xtype: 'textfield',
               anchor: '100%',
                fieldLabel: 'SLA Id',
                hidden: true,
                bind: { value: '{form.slaId}' }
            },
            {
                fieldLabel: 'Category',
                anchor: '100%',
                xtype: 'combo',
                allowBlank: false,
                displayField: 'display',
                valueField: 'id',
                queryMode: 'local',
                forceSelection: true,
                listeners: {
                    scope: 'controller'
                },
                bind: {
                    selection: '{form.subCate}',
                    store: '{ticketSubCateStore}',
                    readOnly: '{isEdit}'
                }

            },{
                fieldLabel: 'Status',
                allowBlank: false,
                anchor: '100%',
                xtype: 'combo',
                forceSelection: true,
                queryMode: 'local',
                displayField: 'display',
                valueField: 'id',
                listeners: {
                    scope: 'controller'
                },
                bind: {
                    selection: '{form.status}',
                    store: '{statusStore}',
                    readOnly: '{isEdit}'
                }
            }]
        }];
    }

});
