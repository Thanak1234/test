
Ext.define("Workflow.view.ticket.setting.grouppolicy.TicketGroupPolicyForm",{
    extend: "Ext.panel.Panel",

    requires: [
        "Workflow.view.ticket.setting.grouppolicy.TicketGroupPolicyFormController",
        "Workflow.view.ticket.setting.grouppolicy.TicketGroupPolicyFormModel"
    ],

    controller: "ticket-setting-grouppolicy-ticketgrouppolicyform",
    viewModel: {
        type: "ticket-setting-grouppolicy-ticketgrouppolicyform"
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
            defaults: {
                anchor: '100%',
                labelWidth : 140
            },
            labelWidth: 600,
            border: true,
            bodyPadding: '10 10 0',
            margin: 5,
            defaultType: 'checkboxfield',
            items: [{
                xtype: 'textfield',
                //anchor: '100%',
                allowBlank: false,
                fieldLabel: 'Name',
                bind: {
                    value: '{form.groupName}',
                    readOnly: '{isEdit}'
                }
            }, {
                fieldLabel: 'Status',
                allowBlank: false,
                //anchor: '100%',
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
            },
            {
                xtype: 'panel',
                layout: 'hbox',
                margin: '0 0 10 0',
                items:[
                    {
                        fieldLabel: 'Limit Access',
                        allowBlank: false,
                        //anchor: '100%',
                        xtype: 'combo',
                        labelWidth : 140,
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'display',
                        valueField: 'id',
                        listeners: {
                            scope: 'controller'
                        },
                        bind: {
                            selection: '{form.limitAccess}',
                            store: '{limitAccessStore}',
                            readOnly: '{isEdit}',
                            value: '{customLimitAccessFocusValue}'
                        }
                    },
                    {
                        xtype: 'button',
                        margin: '0 0 0 10',
                        text: 'Custom',
                        handler: 'onAddCustomLimitAccessHandler'

                    }
                ]
            },

            {
                xtype: 'panel',
                layout: 'hbox',
                margin: '0 0 10 0',
                items:[
                    {
                        fieldLabel: 'Report Limit Access',
                        allowBlank: false,
                        //anchor: '100%',
                        xtype: 'combo',
                        labelWidth : 140,
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'display',
                        valueField: 'id',
                        listeners: {
                            scope: 'controller'
                        },
                        bind: {
                            selection: '{form.reportLimitAccess}',
                            store: '{reportLimitAccessStore}',
                            readOnly: '{isEdit}',
                            value: '{customReportLimitAccessFocusValue}'
                        }
                    },
                    {
                        xtype: 'button',
                        margin: '0 0 0 10',
                        text: 'Custom',
                        handler: 'onAddCustomReportLimitAccessHandler'

                    }
                ]
            },

            ,{
                fieldLabel: 'New Ticket Notify',
                allowBlank: false,
                //anchor: '100%',
                xtype: 'combo',
                forceSelection: true,
                queryMode: 'local',
                displayField: 'display',
                valueField: 'id',
                listeners: {
                    scope: 'controller'
                },
                bind: {
                    selection: '{form.newTicketNotify}',
                    store: '{newTicketNotifyStore}',
                    readOnly: '{isEdit}'
                }
            }, {
                fieldLabel: 'Assigned Notify',
                allowBlank: false,
                //anchor: '100%',
                xtype: 'combo',
                forceSelection: true,
                queryMode: 'local',
                displayField: 'display',
                valueField: 'id',
                listeners: {
                    scope: 'controller'
                },
                bind: {
                    selection: '{form.assignedNotify}',
                    store: '{assignedNotifyStore}',
                    readOnly: '{isEdit}'
                }
            }, {
                fieldLabel: 'Reply Notify',
                allowBlank: false,
                //anchor: '100%',
                xtype: 'combo',
                forceSelection: true,
                queryMode: 'local',
                displayField: 'display',
                valueField: 'id',
                listeners: {
                    scope: 'controller'
                },
                bind: {
                    selection: '{form.replyNotify}',
                    store: '{replyNotifyStore}',
                    readOnly: '{isEdit}'
                }
            }, {
                fieldLabel: 'Change Status Notify',
                allowBlank: false,
                //anchor: '100%',
                xtype: 'combo',
                forceSelection: true,
                queryMode: 'local',
                displayField: 'display',
                valueField: 'id',
                listeners: {
                    scope: 'controller'
                },
                bind: {
                    selection: '{form.changeStatusNotify}',
                    store: '{changeStatusNotifyStore}',
                    readOnly: '{isEdit}'
                }
            },{
                xtype: 'fieldset',
                style: {
                    backgroundColor: 'white',
                    borderColor: 'white',
                    borderStyle: 'solid'
                },
                defaultType: 'checkboxfield',
                layout: {
                    type: 'column',
                    align: 'stretch'
                },
                items: [{
                    xtype: 'panel',
                    columnWidth: 0.5,
                    anchor: '100%',
                    defaultType: 'checkboxfield',
                    items: [{
                        boxLabel  : 'Create Ticket',
                        name      : 'createTicket',
                        inputValue: '1',
                        hidden:true,
                        bind: {
                            value: '{form.createTicket}',
                            readOnly: '{isEdit}'
                        }
                    },{
                        boxLabel  : 'Edit Ticket',
                        name      : 'editTicket',
                        inputValue: '1',
                        bind: {
                            value: '{form.editTicket}',
                            readOnly: '{isEdit}'
                        }
                    },{
                        boxLabel  : 'Edit Requestor',
                        name      : 'editRequestor',
                        hidden    : true,
                        inputValue: '1',
                        bind: {
                            value: '{form.editRequestor}',
                            readOnly: '{isEdit}'
                        }
                    },{
                        boxLabel  : 'Department Transfer',
                        name      : 'deptTransfer',
                        hidden    : true,
                        inputValue: '1',
                        bind: {
                            value: '{form.deptTransfer}',
                            readOnly: '{isEdit}'
                        }
                    },{
                        boxLabel  : 'Change Status',
                        name      : 'changeStatus',
                        inputValue: '1',
                        bind: {
                            value: '{form.changeStatus}',
                            readOnly: '{isEdit}'
                        }
                    },{
                        boxLabel  : 'Post Ticket',
                        name      : 'postTicket',
                        hidden:true,
                        inputValue: '1',
                        bind: {
                            value: '{form.postTicket}',
                            readOnly: '{isEdit}'
                        }
                    },
                    {
                        boxLabel  : 'Assign Ticket',
                        name      : 'assignTicket',
                        inputValue: '1',
                        bind: {
                            value: '{form.assignTicket}',
                            readOnly: '{isEdit}'
                        }
                    }]
                },{
                    xtype: 'panel',
                    columnWidth: 0.5,
                    anchor: '100%',
                    defaultType: 'checkboxfield',
                    items: [{
                        boxLabel  : 'Close Ticket',
                        name      : 'closeTicket',
                        hidden:true,
                        inputValue: '1',
                        bind: {
                            value: '{form.closeTicket}',
                            readOnly: '{isEdit}'
                        }
                    },
                    {
                        boxLabel  : 'Merge Ticket',
                        name      : 'mergeTicket',
                        inputValue: '1',
                        bind: {
                            value: '{form.mergeTicket}',
                            readOnly: '{isEdit}'
                        }
                    },
                    {
                        boxLabel  : 'Delete Ticket',
                        name      : 'deleteTicket',
                        inputValue: '1',
                        bind: {
                            value: '{form.deleteTicket}',
                            readOnly: '{isEdit}'
                        }
                    },
                    {
                        boxLabel  : 'Create Sub Ticket',
                        name      : 'subTicket',
                        inputValue: '1',
                        bind: {
                            value: '{form.subTicket}',
                            readOnly: '{isEdit}'
                        }
                    }]
                }]
            }, {
                xtype: 'textarea',
                anchor: '100%',
                fieldLabel: 'Description',
                bind: { value: '{form.description}' }
            }]
        }];
    }

});
