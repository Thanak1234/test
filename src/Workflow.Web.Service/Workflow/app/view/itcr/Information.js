Ext.define("Workflow.view.itcr.Information", {
    extend: "Ext.form.Panel",
    xtype: 'itcr-information',
    border: false,
    controller: true,
    viewModel: {
        data: {
            restorationLavelLabel: null
        }
    },
    layout: {
        type: 'vbox',
        pack: 'start',
        align: 'stretch'
    },
    margin: '0 0 0 0',
    defaults: {
        xtype: 'panel',
        width: '100%',
        margin: '5 0',
        collapsible: true,
        iconCls: 'fa fa-cogs',
        border: true
    },
    initComponent: function () {
        var me = this;
        me.mkItems();
        me.callParent(arguments);
    },
    mkItems: function() {
        var me = this;
        me.items = [
            me.mkRequestChange(),
            me.mkPlan(),
            me.mkTesting(),
            me.mkManagerAcknowledge()
        ];
    },
    mkRequestChange: function () {
        return {
            xtype: 'panel',
            title: 'Request Change',
            layout: 'column',
            defaults: {
                xtype: 'textarea',
                width: '100%',
                margin: '5 10',
                columnWidth: 1,
                labelWidth: 185
            },
            items: [{
                xtype: 'datefield',
                altFormats: 'c',
                format: 'd/m/Y',
                columnWidth: .5,
                allowBlank: false,
                fieldLabel: 'Date Request',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.dateRequest}'
                }
            }, {
                xtype: 'datefield',
                altFormats: 'c',
                format: 'd/m/Y',
                columnWidth: .5,
                fieldLabel: 'Target Date',
                allowBlank: false,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.targetDate}'
                }
            }, {
                xtype: 'ngLookup',
                fieldLabel: 'IT Session',
                url: 'api/lookup/it/sessions',
                allowBlank: false,
                displayField: 'name',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.session}'
                }
            }, {
                xtype: 'ngLookup',
                fieldLabel: 'Type of Change Request',
                url: 'api/lookup/itcr/change_request_types',
                allowBlank: false,
                displayField: 'name',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.changeType}'
                }
            }, {
                xtype: 'label',
                text: 'Request Change:'
            }, {
                emptyText: 'Detail about the request',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.requestChange}'
                }
            }, {
                xtype: 'label',
                text: 'Justification/Objective:'
            }, {
                emptyText: 'What are the reasons for the change',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.justification}'
                }
            }]
        };
    },
    mkPlan: function () {
        me = this;
        return {
            xtype: 'panel',
            title: 'Plan',
            layout: 'column',
            defaults: {
                xtype: 'textarea',
                width: '100%',
                margin: '5 10',
                columnWidth: 1
            },
            items: [{
                xtype: 'label',
                text: 'Implementation:'
            }, {
                emptyText: 'Detail step by step breakdown of  the change plan',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.implmentation}'
                }
            }, {
                xtype: 'label',
                text: 'Failback:'
            }, {
                emptyText: 'Detail breakdown of step to reverse change',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.failback}'
                }
            }, {
                xtype: 'label',
                text: 'System Restoration Levels'
            }, {
                xtype: 'combobox',
                columnWidth: .2,
                displayField: 'name',
                valueField: 'name',
                editable: false,
                queryMode: 'local',
                store: Ext.create('Ext.data.Store', {
                    autoLoad: true,
                    proxy: {
                        type: 'ajax',
                        url: '/data/itcr-restore-levels.json'
                    }
                }),
                listeners: {
                    change: function(cb, value){
                        var refs = me.getReferences(),
                            store = cb.getStore();
                        var rec = store.findRecord('name', value);

                        if(rec){
                            refs.restorationLavelLabel.setText(rec.get('label'));
                        }
                    }
                },
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.restorationLavel}'
                }
            }, {
                xtype: 'label',
                padding: '10 0 0 0',
                margin: 0,
                reference: 'restorationLavelLabel',
                columnWidth: .8,
                bind: {
                     text: '{restorationLavelLabel}'
                }
            },{
                xtype: 'label',
                text: 'Details of required work for manual intervention if any:'
            }, {
                emptyText: 'Details of required work for manual intervention if any',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.intervention}'
                }
            }]
        };
    },
    mkTesting: function () {
        return {
            xtype: 'panel',
            title: 'Testing',
            layout: 'column',
            defaults: {
                xtype: 'textarea',
                width: '100%',
                margin: '5 10',
                columnWidth: 1
            },
            items: [{
                xtype: 'label',
                text: 'Desired Result:'
            }, {
                emptyText: 'Enter Desired results of the test here\ne.g.\n1. Password field should have complexity requirement',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.direedResult}'
                }
            }, {
                xtype: 'label',
                text: 'Test Parameters:'
            }, {
                emptyText: 'e.g. \n 1. Password 12345, no letters no symbol \n 2. Password **abcde123, alpha-num and symbols',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.testParameters}'
                }
            }, {
                xtype: 'label',
                text: 'Actual Result:'
            }, {
                emptyText: 'Password rejected \nPassword accepted',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.actualResult}'
                }
            }, {
                xtype: 'label',
                text: 'Additional Notes:'
            }, {
                emptyText: 'Additional notes if any',
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{formData.additionalNotes}'
                }
            }]
        };
    },
    mkManagerAcknowledge: function () {
        return {
            xtype: 'panel',
            title: 'Manager Acknowledge',
            layout: 'column',            
            defaults: {
                xtype: 'textarea',
                width: '100%',
                margin: '5 10',
                columnWidth: 1
            },
            items: [{
                xtype: 'ngLookup',
                fieldLabel: 'Result',
                url: 'api/lookup/itcr/results',
                allowBlank: false,
                displayField: 'name',
                bind: {
                    readOnly: '{config.acknowledge.readOnly}',
                    value: '{formData.akResult}'
                }
            }, {
                xtype: 'textarea',
                fieldLabel: 'Remark',
                allowBlank: false,
                bind: {
                    readOnly: '{config.acknowledge.readOnly}',
                    value: '{formData.akRemark}'
                }
            }, {
                xtype: 'label',
                text: 'Note: Failed/Reworked need to attached original form in PDF format. if rollback based on previous change need to attached the original form.'
            }],
            bind: {
                hidden: '{config.acknowledge.hidden}',
                disabled: '{config.acknowledge.disabled}'
            }
        };
    }
});
