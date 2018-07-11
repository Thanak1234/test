Ext.define("Workflow.view.ticket.setting.sla.TicketSlaMappingForm",{
    extend: "Ext.panel.Panel",

    requires: [
        "Workflow.view.ticket.setting.sla.TicketSlaMappingFormController",
        "Workflow.view.ticket.setting.sla.TicketSlaMappingFormModel"
    ],

    controller: "ticket-setting-sla-ticketslamappingformcontroller",
    viewModel: {
        type: "ticket-setting-sla-ticketslamappingformmodel"
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
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                        xtype: 'combo',
                        fieldLabel: 'Type',
                        //labelAlign: 'right',
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'typeName',
                        valueField: 'id',
                        listeners: {
                            scope: 'controller'
                        },
                        bind: {
                            store: '{ticketTypeStore}',
                            selection: '{form.type}',
                            readOnly: '{isEdit}',
                            //value: '{minNumDayResolution}'
                        },
                        allowBlank: false,
                        listConfig: {
                            minWidth: 200,
                            //resizable: true,
                            //loadingText: 'Searching...',
                            //itemSelector: '.search-item',
                            itemTpl: [
                                '<div><img src="{icon}"/><span style="margin-left: 10px!important;">{typeName}</span></div>'
                            ]
                        }
                },{
                        xtype: 'combo',
                        fieldLabel: 'Priority',
                        //labelAlign: 'right',
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'priorityName',
                        valueField: 'id',
                        listeners: {
                            scope: 'controller'
                        },
                        bind: {
                            store: '{ticketPriorityStore}',
                            selection: '{form.priority}',
                            readOnly: '{isEdit}',
                            //value: '{minNumDayResolution}'
                        },
                        allowBlank: false
                },{
                        xtype: 'combo',
                        fieldLabel: 'SLA',
                        //labelAlign: 'right',
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'slaName',
                        valueField: 'id',
                        listeners: {
                            scope: 'controller',
                            select: 'onSlaChangeHandler'
                        },
                        bind: {
                            store: '{ticketSlaStore}',
                            selection: '{form.sla}',
                            readOnly: '{isEdit}',
                            //value: '{minNumDayResolution}'
                        },
                        listConfig: {
                            minWidth: 500,
                            resizable: true,
                            loadingText: 'Searching...',
                            itemSelector: '.search-item',
                            itemTpl: [
                                '<a >',
                                '<h3><span>{slaName}</span></h3>',
                                'Resolution Time: {gracePeriodFormated} | Response Time: {firstResponsePeriodFormated}',
                                '</a>'
                            ]
                        },
                        allowBlank: false
                }, {
                    xtype: 'textarea',
                    anchor: '100%',
                    fieldLabel: 'Description',
                    bind: { value: '{form.description}' }
                }
            ]
        }];
    }

});
