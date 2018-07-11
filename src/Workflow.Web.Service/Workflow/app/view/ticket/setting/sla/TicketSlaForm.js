
Ext.define("Workflow.view.ticket.setting.sla.TicketSlaForm",{
    extend: "Ext.panel.Panel",

    requires: [
        "Workflow.view.ticket.setting.sla.TicketSlaFormController",
        "Workflow.view.ticket.setting.sla.TicketSlaFormModel"
    ],

    controller: "ticket-setting-sla-ticketslaform",
    viewModel: {
        type: "ticket-setting-sla-ticketslaform"
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
                fieldLabel: 'SLA Name',
                bind: { 
                    value: '{form.slaName}',
                    readOnly: '{isEdit}' 
                }
            },{
                xtype: 'fieldset',
                layout: 'hbox',
                title: 'Resolution Time',
                items:[
                    {
                        xtype: 'combo',      
                        fieldLabel: 'Days',
                        labelAlign: 'right',                                          
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'num',                
                        valueField: 'id',
                        listeners: {                    
                            scope: 'controller'                    
                        },
                        
                        bind: {
                            selection: '{form.numDayResolution}',
                            store: '{dayStoreResolution}',
                            readOnly: '{isEdit}',
                            value: '{minNumDayResolution}'
                        }
                    },
                    {
                        xtype: 'combo',      
                        fieldLabel: 'Hours',
                        labelAlign: 'right',                                          
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'num',                
                        valueField: 'id',                        
                        listeners: {                    
                            scope: 'controller'                    
                        },
                        bind: {
                            selection: '{form.numHourResolution}',
                            store: '{hourStoreResolution}',
                            readOnly: '{isEdit}',
                            value: '{minNumHourResolution}'
                        }
                    },
                    {
                        xtype: 'combo',      
                        fieldLabel: 'Minutes',        
                        labelAlign: 'right',                                  
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'num',                
                        valueField: 'id',                        
                        listeners: {                    
                            scope: 'controller'                    
                        },
                        bind: {
                            selection: '{form.numMinuteResolution}',
                            store: '{minuteStoreResolution}',
                            readOnly: '{isEdit}',
                            value: '{minNumMinuteResolution}'
                        }
                    }
                ]
            },{
                xtype: 'fieldset',
                layout: 'hbox',
                title: 'Response Time',
                items:[
                    {
                        xtype: 'combo',      
                        fieldLabel: 'Days',
                        labelAlign: 'right',                                          
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'num',                
                        valueField: 'id',
                        listeners: {                    
                            scope: 'controller'                    
                        },
                        bind: {
                            selection: '{form.numDayResponse}',
                            store: '{dayStoreResponse}',
                            readOnly: '{isEdit}',
                            value: '{minNumDayResponse}'
                        }
                    },
                    {
                        xtype: 'combo',      
                        fieldLabel: 'Hours',
                        labelAlign: 'right',                                          
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'num',                
                        valueField: 'id',
                        listeners: {                    
                            scope: 'controller'                    
                        },
                        bind: {
                            selection: '{form.numHourResponse}',
                            store: '{hourStoreResponse}',
                            readOnly: '{isEdit}',
                            value: '{minNumHourResponse}'
                        }
                    },
                    {
                        xtype: 'combo',      
                        fieldLabel: 'Minutes',        
                        labelAlign: 'right',                                  
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'num',                
                        valueField: 'id',
                        listeners: {                    
                            scope: 'controller'                    
                        },
                        bind: {
                            selection: '{form.numMinuteResponse}',
                            store: '{minuteStoreResponse}',
                            readOnly: '{isEdit}',
                            value: '{minNumMinuteResponse}'
                        }
                    }
                ]
            },{
                xtype: 'textarea',
                anchor: '100%',
                fieldLabel: 'Description',
                bind: { value: '{form.description}' }
            }]
        }];
    }

});
