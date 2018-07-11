Ext.define('Workflow.view.common.worklist.OutOfficeWindow', {
    extend: 'Workflow.view.common.requestor.AbstractWindowDialog',
    
    controller: 'common-worklist-outofficewindow',    

    title: 'Configure Out of Office',
    closeAction: 'hide',
    height: 600,
    layout: {
        type: 'border',
        padding: 0
    },
    initComponent: function () {
        var me = this;        
        me.buildComponents();
        me.callParent();
    },

    buildComponents: function () {
        var me = this;
        me.items = [
            {
                xtype: 'form',
                region: 'center',                
                defaultType: 'textfield',
                fieldDefaults: {
                    labelWidth: 60
                },

                layout: {
                    type: 'vbox',
                    align: 'stretch'
                },

                bodyPadding: 0,
                border: false,

                items: [
                    {
                        xtype: 'fieldset',
                        flex: 1,
                        defaultType: 'radio',
                        layout: 'anchor',
                        defaults: {
                            anchor: '100%',
                            hideEmptyLabel: false                         
                        },
                        items: [
                            {
                                xtype: 'radiogroup',
                                reference: 'radio',
                                defaults: {
                                    name: 'status'
                                },
                                bind: {
                                    value: '{status}'
                                },
                                fieldLabel: 'I am',
                                labelAlign: 'left',
                                columns: 1,
                                items: [
                                    {
                                        boxLabel: 'In Office',
                                        inputValue: true
                                    },
                                    {
                                        boxLabel: 'Out Office',
                                        inputValue: false
                                    }
                                ],
                                listeners: {
                                    change: 'onStatusChange'
                                }

                            },
                            {
                                xtype: 'common.worklist.components.user-grid',
                                title: 'Forward all work items to',
                                reference: 'destinationGridPanel',
                                bind: {                                    
                                    store: '{destinations}'
                                },
                                height: 200,
                                frame: true
                            },
                            {
                                xtype: 'common.worklist.components.exceptionrule-grid',
                                reference: 'exceptionRuleGridPanel',
                                bind: {                                    
                                    selection: '{selectedException}',
                                    store: '{exceptionRules}'
                                },
                                height: 200
                            }                            
                        ]
                    }
                ]
            }
        ];

        me.buttons = [
            {
                text: 'OK',
                handler: 'onOkClickHandler'
            },
            {
                text: 'Cancel',
                handler: 'onCancelClickHandler'
            }
        ];
    }
});