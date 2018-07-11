Ext.define("Workflow.view.itapp.ProjectApproval", {
    extend: "Ext.form.Panel",
    xtype: 'projectapproval',
    border: true,
    layout: {
        type: 'vbox',
        pack: 'start'
    },
    margin: '0 0 10 0',
    defaults: {
        width: '100%',
        margin: 10
    },
    width: '100%',
    collapsible: true,
    title: 'Project Approval',
    iconCls: 'fa fa-cogs',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'fieldset',
                title: 'Cost Estimates',
                defaults: {
                    xtype: 'currencyfield',
                    minValue: 0,
                    format: '$0,000.00',
                    value: 0,
                    allowExponential: false,
                    width: '100%',
                    labelWidth: 245
                },
                items: [                    
                    {
                        fieldLabel: 'Hardware cost',
                        allowBlank: false,
                        bind: {
                            value: '{ProjectApproval.Hc}',
                            readOnly: '{readOnlyProjectApproval}'
                        }
                    },
                    {
                        fieldLabel: 'Software license cost',                        
                        allowBlank: false,
                        bind: {
                            value: '{ProjectApproval.Slc}',
                            readOnly: '{readOnlyProjectApproval}'
                        }
                    },
                    {
                        xtype: 'numberfield',
                        minValue: 0,
                        value: 0,
                        fieldLabel: 'Service cost in man-days(working day)',
                        allowBlank: false,
                        allowDecimals: false,
                        bind: {
                            value: '{ProjectApproval.Scmd}',
                            readOnly: '{readOnlyProjectApproval}'
                        }
                    }
                ]
            },
            {
                xtype: 'fieldset',
                title: 'Impact',
                defaults: {
                    xtype: 'textfield',
                    width: '100%',
                    labelWidth: 245
                },
                items: [
                    {
                        fieldLabel: 'Resource or Schedule impact, if any',
                        bind: {
                            value: '{ProjectApproval.Rsim}',
                            readOnly: '{readOnlyProjectApproval}'
                        }
                    },
                    {
                        fieldLabel: 'Risk or assumption that we made, if any',
                        bind: {
                            value: '{ProjectApproval.Rawm}',
                            readOnly: '{readOnlyProjectApproval}'
                        }
                    }
                ]
            },
            {
                xtype: 'textfield',
                labelWidth: 260,
                fieldLabel: 'Target Delivery Date',
                allowBlank: false,
                bind: {
                    value: '{ProjectApproval.DeliveryDate}',
                    readOnly: '{readOnlyProjectApproval}'
                }
            },
            {
                xtype: 'datefield',
                labelWidth: 260,
                fieldLabel: 'Go Live Date',
                format: 'd/m/Y',
                altFormats: 'c',
                reference: 'golivedf',
                bind: {
                    value: '{ProjectApproval.GoLiveDate}',
                    hidden: '{hideGoLiveDate}',
                    readOnly: '{readOnlyGoLiveDate}'
                }
            }
        ];

        me.callParent(arguments);
    }
});
