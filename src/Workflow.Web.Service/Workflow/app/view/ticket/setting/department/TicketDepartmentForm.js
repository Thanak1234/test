
Ext.define("Workflow.view.ticket.setting.department.TicketDepartmentForm",{
    extend: "Ext.panel.Panel",

    requires: [
        "Workflow.view.ticket.setting.department.TicketDepartmentFormController",
        "Workflow.view.ticket.setting.department.TicketDepartmentFormModel"
    ],

    controller: "ticket-setting-department-ticketdepartmentform",
    viewModel: {
        type: "ticket-setting-department-ticketdepartmentform"
    },
    scrollable : 'y',
    frame: true,
    layout: {
        type: 'anchor',
        pack: 'start',
        align: 'stretch'
    },
    resizable: false,
    modal: true,
    
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
                    value: '{form.deptName}',
                    readOnly: '{isEdit}'
                }
            },{
                xtype: 'textfield',
                anchor: '100%',
                allowBlank: false,
                fieldLabel: 'Automation Email',
                bind: {
                    value: '{form.automationEmail}',
                    readOnly: '{isEdit}'
                }
            } ,{
                fieldLabel: 'Default Item',
                allowBlank: false,
                anchor: '100%',
                xtype: 'combo',
                forceSelection: true,
                queryMode: 'local',
                displayField: 'itemName',
                valueField: 'id',
                listeners: {
                    scope: 'controller'
                },
                bind: {
                    selection: '{form.item}',
                    store: '{defaultItemStore}',
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
            }, {
                xtype: 'textarea',
                anchor: '100%',
                fieldLabel: 'Description',
                bind: { value: '{form.description}' }
            }]
        }];
    }

});
