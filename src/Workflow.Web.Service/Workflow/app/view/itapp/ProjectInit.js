Ext.define("Workflow.view.itapp.ProjectInit", {
    extend: "Ext.form.Panel",
    xtype: 'projectinit',
    border: true,
    layout: {
        type: 'vbox',
        pack: 'start'
    },
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        width: '100%',
        margin: 10,
        labelWidth: 110
    },
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'Project Initiation (By Requestor)',
    initComponent: function () {
        var me = this;
        var arrayData = [
        ['Bally Application (SDS, CMP, CAGE, TABLEVIEW), BPSConnect and ICS', 'Casino Applications'],
        ['OPERA, SIMPHONY, MATERIAL CONTROL, VINGCARD, JDS and SUN', 'Hotel Applications'],
        ['K2', 'Back Office Applications']
        ];
        var store = Ext.create('Ext.data.ArrayStore', {
            data : arrayData,
            fields: ['display', 'value']
        });
        me.items = [
            {
                xtype: 'combo',
                fieldLabel: 'Application',
                allowBlank: false,
                editable: false,
                readOnly: false,
                store: store,
                valueField: 'value',
                displayField: 'value',
                bind: {
                    selection: '{selectedRecord}',
                    value: '{ProjectInit.Application}',
                    readOnly: '{readOnlyProjectInit}'
                }
            },
            {
                xtype: 'label',
                margin: '0 0 10 125',
                bind: {
                    html: '<span style="color: red">{selectedRecord.display}</span>'
                }                
            },
            {
                fieldLabel: 'Proposed Change',
                allowBlank: false,
                readOnly: false,
                bind: {
                    value: '{ProjectInit.ProposedChange}',
                    readOnly: '{readOnlyProjectInit}'
                }
            },
            {
                xtype: 'textareafield',
                fieldLabel: 'Description',
                allowBlank: false,
                readOnly: false,
                bind: {
                    value: '{ProjectInit.Description}',
                    readOnly: '{readOnlyProjectInit}'
                }
            },
            {
                xtype: 'fieldset',
                title: 'Benefits of Change',
                layout: 'fit',
                defaults: {
                    xtype: 'checkbox'
                },
                items: [
                    {
                        xtype: 'checkboxgroup',
                        columns: 3,
                        vertical: true,
                        items: [
                            {
                                boxLabel: 'Cost Savings', name: 'rb', inputValue: '1',
                                bind: {
                                    value: '{ProjectInit.BenefitCS}',
                                    readOnly: '{readOnlyProjectInit}'
                                }
                            },
                            {
                                boxLabel: 'Increase In Sales', name: 'rb', inputValue: '2',
                                bind: {
                                    value: '{ProjectInit.BenefitIIS}',
                                    readOnly: '{readOnlyProjectInit}'
                                }
                            },
                            {
                                boxLabel: 'Risk Management', name: 'rb', inputValue: '3',
                                bind: {
                                    value: '{ProjectInit.BenefitRM}',
                                    readOnly: '{readOnlyProjectInit}'
                                }
                            }
                        ]
                    },
                    {
                        xtype: 'textfield',
                        fieldLabel: 'Others (Please specify)',
                        labelWidth: 150,
                        bind: {
                            value: '{ProjectInit.BenefitOther}',
                            readOnly: '{readOnlyProjectInit}'
                        }
                    }
                ]
            },
            {
                xtype: 'fieldset',
                title: 'Priority Consideration',
                layout: 'fit',
                items: [
                    {
                        xtype: 'currencyfield',
                        fieldLabel: 'Benefits in USD (potential cost savings or increase of revenue)',
                        labelWidth: 200,
                        minValue: 0,
                        format: '$0,000.00',
                        value: 0,
                        allowExponential: false,
                        bind: {
                            value: '{ProjectInit.PriorityConsideration}',
                            readOnly: '{readOnlyProjectInit}'
                        }
                    }
                ]
            }
        ];

        me.callParent(arguments);
    }
});
