Ext.define("Workflow.view.reports.ccr.CCRReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-ccrcriteria',
    title: 'Contract Draft/Review Request - Criteria',
    viewModel: {
        type: "report-ccr"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Information',
            layout: 'vbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                margin: '0 5 0 0'
            },
            items: [
                {
                    xtype: 'fieldcontainer',
                    layout: 'hbox',
                    margin: '5 0 5 0',
                    defaults: {
                        flex: 1,
                        hideLabel: true,
                        width: 250,
                        margin: '0 5 0 0',
                        xtype: 'textfield',
                        triggers: {
                            clear: {
                                type: 'clear',
                                hideWhenEmpty: true,
                                clearOnEscape: true,
                                weight: -1
                            }
                        }
                    },
                    items: [
                        {
                            margin: '0 5 0 0',
                            emptyText: 'BCJ NUMBER',
                            bind: {
                                value: '{criteria.Bcj}'
                            }
                        },
                        {
                            emptyText: 'NAME OF CONTRACTING PARTY',
                            margin: '0 5 0 0',
                            bind: {
                                value: '{criteria.Name}'
                            }
                        }
                    ]
                },                
                {
                    xtype: 'fieldcontainer',
                    layout: 'hbox',
                    margin: '5 0 5 0',
                    defaults: {
                        flex: 1,
                        hideLabel: true,
                        width: 250,
                        margin: '0 5 0 0',
                        xtype: 'textfield',
                        triggers: {
                            clear: {
                                type: 'clear',
                                hideWhenEmpty: true,
                                clearOnEscape: true,
                                weight: -1
                            }
                        }
                    },
                    items: [
                        {
                            emptyText: 'REGISTRATION NO',
                            margin: '0 5 0 0',
                            bind: {
                                value: '{criteria.RegisterNo}',
                            }
                        },
                        {
                            emptyText: 'CONTACT NAME',
                            bind: {
                                value: '{criteria.ContactName}',
                            }
                        },
                        {
                            margin: '0 5 0 0',
                            emptyText: 'ISSUEED BY(COUNTRY)',
                            bind: {
                                value: '{criteria.IssueBy}'
                            }
                        }                        
                    ]
                },
                {
                    xtype: 'fieldset',
                    columnWidth: 1,
                    title: 'CONTRACT STATUS',
                    defaultType: 'checkbox',
                    layout: 'column',
                    checkboxToggle: true,
                    collapsed: true,
                    width: 910,
                    defaults: {
                        margin: '0 35 0 0'
                    },
                    listeners: {
                        collapse: function (fieldset, eOpts) {
                            var viewmodel = me.getViewModel();
                            viewmodel.set('criteria.StatusNew', null);
                            viewmodel.set('criteria.StatusRenewal', null);
                            viewmodel.set('criteria.StatusReplacement', null);
                            viewmodel.set('criteria.StatusAddendum', null);
                        }
                    },
                    items: [
                        {
                            boxLabel: 'New',
                            bind: {
                                value: '{criteria.StatusNew}',
                                readOnly: '{readOnly}'
                            }
                        },
                        {
                            boxLabel: 'Renewal',
                            bind: {
                                value: '{criteria.StatusRenewal}',
                                readOnly: '{readOnly}'
                            }
                        },
                        {
                            boxLabel: 'Replacement',
                            bind: {
                                value: '{criteria.StatusReplacement}',
                                readOnly: '{readOnly}'
                            }
                        },
                        {
                            boxLabel: 'Addendum',
                            bind: {
                                value: '{criteria.StatusAddendum}',
                                readOnly: '{readOnly}'
                            }
                        }
                    ]
                },
                {
                    xtype: 'fieldset',
                    columnWidth: 1,
                    title: 'AGREEMENT TYPE',
                    defaultType: 'checkbox',
                    layout: 'column',
                    checkboxToggle: true,
                    collapsed: true,
                    width: 910,
                    defaults: {
                        margin: '0 2 0 0'
                    },
                    listeners: {
                        collapse: function (fieldset, eOpts) {
                            var viewmodel = me.getViewModel();
                            viewmodel.set('criteria.AtSa', null);
                            viewmodel.set('criteria.AtSpa', null);
                            viewmodel.set('criteria.AtLa', null);
                            viewmodel.set('criteria.AtCa', null);
                            viewmodel.set('criteria.AtLea', null);
                            viewmodel.set('criteria.AtEa', null);                            
                            viewmodel.set('criteria.AtOther', null);
                        }
                    },
                    items: [
                        {
                            boxLabel: 'Services Agreement',
                            bind: {
                                value: '{criteria.AtSa}',
                                readOnly: '{readOnly}'
                            }
                        },
                        {
                            boxLabel: 'Sale & Purchase Agreement',
                            bind: {
                                value: '{criteria.AtSpa}',
                                readOnly: '{readOnly}'
                            }
                        },
                        {
                            boxLabel: 'Licence Agreement',
                            bind: {
                                value: '{criteria.AtLa}',
                                readOnly: '{readOnly}'
                            }
                        },
                        {
                            boxLabel: 'Corporate Agreement',
                            bind: {
                                value: '{criteria.AtCa}',
                                readOnly: '{readOnly}'
                            }
                        },
                        {
                            boxLabel: 'Lease Agreement',
                            bind: {
                                value: '{criteria.AtLea}',
                                readOnly: '{readOnly}'
                            }
                        },
                        {
                            boxLabel: 'Entertainment Agreement',
                            bind: {
                                value: '{criteria.AtEa}',
                                readOnly: '{readOnly}'
                            }
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: 'Others',
                            labelWidth: 50,
                            width: 860,
                            bind: {
                                value: '{criteria.AtOther}',
                                readOnly: '{readOnly}'
                            }
                        }
                    ]
                }
            ]
        });

        return fields;
    }
});