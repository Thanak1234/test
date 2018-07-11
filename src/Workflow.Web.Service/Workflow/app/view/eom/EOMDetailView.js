Ext.define("Workflow.view.eom.EOMDetailView", {
    extend: "Ext.form.Panel",
    xtype: 'eom-eomdetailview',
    controller: "eom-eomdetailview",
    viewModel: {
        type: "eom-eomdetailview"
    },
    border: false,
    minHeight: 100,
    layout: 'column',
    autoWidth: true,
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'panel',
                layout: 'column',
                collapsible: true,
                border: true,
                iconCls: 'fa fa-file-text-o',
                title: 'RATING: 1 = Very Good 2 = Excellent 3 = Extraordinary',
                columnWidth: 1,
                defaultType: 'combobox',
                defaults: {
                    columnWidth: 1,
                    margin: 10,
                    labelWidth: 780,
                    //labelStyle: 'font-size: 12pt',
                    store: [1, 2, 3],
                    editable: false
                },
                items: [
                    {
                        xtype: 'monthfield',
                        reference: 'month',
                        columnWidth: 0.4,
                        labelWidth: 190,
                        format: 'F, Y',
                        editable: false,
                        fieldLabel: 'Employee of the month',
                        allowBlank: false,
                        bind: {
                            value: '{eomInfo.month}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        xtype: 'container',
                        layout: 'column',
                        items: [
                            {
                                xtype: 'label',
                                text: 'Competency/Attributes',
                                style: 'font-size: 11pt; font-weight: bold;',
                                columnWidth: .92
                            },
                            {
                                xtype: 'label',
                                text: 'Score',
                                style: 'font-size: 11pt; font-weight: bold;',
                                textAlign: 'right'
                            }
                        ]
                    },
                    {
                        fieldLabel: '1. Aligning performance & result driven',
                        labelSeparator: '',
                        triggers: {
                            clear: {
                                type: 'clear',
                                hideWhenEmpty: false,
                                clearOnEscape: true,
                                weight: -1
                            }
                        },
                        bind: {
                            value: '{eomInfo.aprd}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        fieldLabel: '2. Customer focus (internal and external)',
                        labelSeparator: '',
                        triggers: {
                            clear: {
                                type: 'clear',
                                hideWhenEmpty: false,
                                clearOnEscape: true,
                                weight: -1
                            }
                        },
                        bind: {
                            value: '{eomInfo.cfie}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        fieldLabel: '3. Leadership & coaching',
                        labelSeparator: '',
                        triggers: {
                            clear: {
                                type: 'clear',
                                hideWhenEmpty: false,
                                clearOnEscape: true,
                                weight: -1
                            }
                        },
                        bind: {
                            value: '{eomInfo.lc}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        fieldLabel: '4. Time management & planning',
                        labelSeparator: '',
                        triggers: {
                            clear: {
                                type: 'clear',
                                hideWhenEmpty: false,
                                clearOnEscape: true,
                                weight: -1
                            }
                        },
                        bind: {
                            value: '{eomInfo.tmp}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        fieldLabel: '5. Problem solving & decision making',
                        labelSeparator: '',
                        triggers: {
                            clear: {
                                type: 'clear',
                                hideWhenEmpty: false,
                                clearOnEscape: true,
                                weight: -1
                            }
                        },
                        bind: {
                            value: '{eomInfo.psdm}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        xtype: 'textfield',
                        fieldLabel: 'Total',
                        labelAlign: 'right',
                        labelWidth: 780,
                        columnWidth: 0.95,
                        margin: '10 0 10 10',
                        readOnly: true,
                        
                        bind: {
                            value: '{total}'
                        }
                    },
                    {
                        xtype: 'label',
                        columnWidth: 0.05,
                        padding: '5 0 0 0',
                        text: '/15',
                        style: 'font-size: 14pt;'
                    },
                    {
                        xtype: 'label',
                        text: '* Kindly choose 5 competencies for nomination',
                        style: 'font-size: 11pt; font-weight: bold; color: green'
                    }
                ]
            },
            {
                xtype: 'panel',
                iconCls: 'fa fa-file-text-o',
                title: 'HOD/Manager supportive reason for nomination <span style="color: red; font-size: 14pt">*</span>',
                margin: '10 0 0 0',
                height: 200,
                border: true,
                collapsible: true,
                layout: 'anchor',
                columnWidth: 1,
                items: [
                    {
                        xtype: 'textarea',
                        anchor: '100% 100%',
                        margin: '20 5',
                        bind: {
                            value: '{eomInfo.reason}',
                            readOnly: '{!editable}'
                        }
                    }
                ]
            },
            {
                xtype: 'panel',
                iconCls: 'fa fa-file-text-o',
                title: 'T&D USE ONLY',
                margin: '10 0 0 0',
                border: true,
                collapsible: true,
                layout: 'column',                
                bind: {
                    hidden: '{hidetd}'
                },
                defaults: {
                    xtype: 'numberfield',
                    margin: '20 10',
                    allowExponential: false,
                    columnWidth: 0.5,
                    minValue: 0
                },
                columnWidth: 1,
                items: [
                    {
                        fieldLabel: 'Cash ($)',
                        bind: {
                            value: '{eomInfo.cash}',
                            readOnly: '{!tdedit}'
                        }
                    },
                    {
                        fieldLabel: 'Voucher ($)',
                        bind: {
                            value: '{eomInfo.voucher}',
                            readOnly: '{!tdedit}'
                        }
                    }
                ]
            }
            
        ];

        me.callParent(arguments);
    }
});
