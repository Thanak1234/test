Ext.define("Workflow.view.ccr.SectionOne", {
    extend: "Ext.form.Panel",
    xtype: 'ccr-section-one',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        width: '100%',
        margin: '5 10',
        columnWidth: .5,
        labelWidth: 250
    },
    width: '100%',
    collapsible: false,
    iconCls: 'fa fa-cogs',
    title: 'SECTION 1 - STANDARD AGREEMENT DETAILS',
    initComponent: function () {
        var me = this;
        var viewmodel = me.parent.getViewModel();

        me.items = [
            {
                fieldLabel: 'Name Of Contracting Party',
                allowBlank: false,
                bind: {
                    value: '{ContractDraft.Name}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'VAT#',
                allowBlank: true,
                bind: {
                    value: '{ContractDraft.Vat}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'Address',
                allowBlank: false,
                bind: {
                    value: '{ContractDraft.Address}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'E-mail',
                vtype: 'email',
                bind: {
                    value: '{ContractDraft.Email}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'Registration No',
                allowBlank: true,
                bind: {
                    value: '{ContractDraft.RegistrationNo}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'Phone',
                allowBlank: true,
                //regex: /^\d+$/,
                //regexText: 'This field must contain number field.',
                bind: {
                    value: '{ContractDraft.Phone}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'Contact Name',
                allowBlank: false,
                bind: {
                    value: '{ContractDraft.ContactName}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'Position Title',
                bind: {
                    value: '{ContractDraft.Position}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'Issueed by(Country)',
                allowBlank: true,
                bind: {
                    value: '{ContractDraft.IssueedBy}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'label',
                html: '<span style="font-style: italic">Agreement Term: (When do you expect to start and end the agreement?)</span>',
                columnWidth: 1
            },
            {
                fieldLabel: 'Term(Year/Month)',
                //regex: /^\d{4}\/(.+)$/,
                //regexText: 'This field must contain year/month format.',
                allowBlank: false,
                columnWidth: .5,
                bind: {
                    value: '{ContractDraft.Term}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'datefield',
                allowBlank: false,
                format: 'd/m/y',
                fieldLabel: 'Starting Date(dd/mm/yyyy)',
                bind: {
                    value: '{ContractDraft.StartDate}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'Total Agreement Value*(Inclusive Tax%)',
                bind: {
                    value: '{ContractDraft.InclusiveTax}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'datefield',
                allowBlank: false,
                format: 'd/m/y',
                fieldLabel: 'Ending Date(dd/mm/yyyy)',
                bind: {
                    value: '{ContractDraft.EndingDate}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'Payment Term',
                bind: {
                    value: '{ContractDraft.PaymentTerm}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'fieldset',
                columnWidth: 1,
                title: 'Agreement Type (Please tick in the appropriate choice) <span class="req" style="color:red">*</span>',
                defaultType: 'checkbox',
                layout: 'column',                
                defaults: {
                    margin: 2
                },
                items: [
                    {
                        boxLabel: 'Services Agreement',
                        bind: {
                            value: '{ContractDraft.AtSa}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        boxLabel: 'Sale & Purchase Agreement',
                        bind: {
                            value: '{ContractDraft.AtSpa}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        boxLabel: 'Licence Agreement',
                        bind: {
                            value: '{ContractDraft.AtLa}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        boxLabel: 'Corporate Agreement',
                        bind: {
                            value: '{ContractDraft.AtCa}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        boxLabel: 'Lease Agreement',
                        bind: {
                            value: '{ContractDraft.AtLea}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        boxLabel: 'Entertainment Agreement',
                        bind: {
                            value: '{ContractDraft.AtEa}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        xtype: 'textfield',
                        fieldLabel: 'If other(Please specify type of Agreement)',
                        labelWidth: 240,
                        width: 850,
                        bind: {
                            value: '{ContractDraft.AtOther}',
                            readOnly: '{readOnly}'
                        }
                    }
                ]
            },
            {
                xtype: 'checkbox',
                fieldLabel: 'Please tick in approporiate status',
                boxLabel: 'New',
                columnWidth: 0.4,
                bind: {
                    value: '{ContractDraft.StatusNew}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'checkbox',
                boxLabel: 'Renewal',
                columnWidth: 0.15,
                bind: {
                    value: '{ContractDraft.StatusRenewal}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'checkbox',
                boxLabel: 'Replacement',
                columnWidth: 0.15,
                bind: {
                    value: '{ContractDraft.StatusReplacement}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'checkbox',
                boxLabel: 'Addendum',
                columnWidth: 0.15,
                bind: {
                    value: '{ContractDraft.StatusAddendum}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'radiogroup',
                fieldLabel: 'Has the Vendor Information Sheet(VIS) been submitted to Legal?',
                name: 'vis',
                labelWidth: 380,
                defaults: {
                    margin: '0 15'
                },
                items: [
                    { boxLabel: 'Yes', inputValue: true },
                    { boxLabel: 'No', inputValue: false }
                ],
                bind: {
                    value: '{radVis}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'label',
                margin: '0 10',
                html: '<span style="font-style: italic">(Legal review/drafting procedure will commence ONLY upon receipt of complete VIS from vendor)</span>',
                columnWidth: 1
            },
            {
                xtype: 'radiogroup',
                name: 'isCapex',
                columnWidth: .18,
                fieldLabel: 'Is this Capex?',
                labelWidth: 99,
                columns: 1,
                items: [
                    { boxLabel: 'Yes', inputValue: true },
                    { boxLabel: 'No', inputValue: false }
                ],
                bind: {
                    value: '{capex}',
                    readOnly: '{readOnly}'
                },
                listeners: {
                    change: function (el, v) {
                        var viewmodel = me.parent.getViewModel();
                        if (v && v.isCapex == true) {
                            viewmodel.set('ContractDraft.ActA', null);
                            viewmodel.set('ContractDraft.ActB', null);
                            viewmodel.set('ContractDraft.ActC', null);
                            viewmodel.set('ContractDraft.ActD', null);
                        } else {
                            viewmodel.set('ContractDraft.BcjNumber', null);
                        }
                    }
                }
            },
            {
                allowBlank: false,
                fieldLabel: 'If \'Yes\', please state approved BCJ number',
                columnWidth: .45,
                maxLength: 10,
                bind: {
                    disabled: '{!capex.isCapex}',
                    readOnly: '{readOnly}',
                    value: '{ContractDraft.BcjNumber}'
                }
            },
            {
                xtype: 'button',
                columnWidth: .25,
                text: 'Please attach approved BCJ',
                listeners: {
                    click: function (el) {
                        var refs = me.parent.getReferences();
                        refs.fileUpload.getController().addNewFile();
                    }
                },
                bind: {
                    disabled: '{readOnly || !capex.isCapex}'
                }
            },
            {
                xtype: 'label',
                columnWidth: .42,
                text: 'If \'No\', please fill out Section 2 below (Agreement Commercial Terms)'
            },
            {
                xtype: 'label',
                columnWidth: .38,
                html: '<span style="color: red">Attached files can be reviewed at the bottom of the page.</span>'
            }
        ];

        me.callParent(arguments);
    }
});
