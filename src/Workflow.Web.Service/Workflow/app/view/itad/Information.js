Ext.define("Workflow.view.itad.Information", {
    extend: "Ext.form.Panel",
    xtype: 'itad-information',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        width: '100%',
        margin: '5 10',
        columnWidth: .5,
        labelWidth: 88
    },
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'User Information',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'combo',
                fieldLabel: 'Employee ID',
                columnWidth: .4,
                store: {
                    autoLoad: true,
                    proxy: {
                        type: 'rest',
                        url: Workflow.global.Config.baseUrl +
                            'api/lookup/employees',
                        reader: {
                            type: 'json'
                        }
                    }
                },
                queryMode: 'local',
                minChars: 0,
                editable: true,
                displayField: 'value',
                valueField: 'employeeNo',
                allowBlank: false,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.employeeNo}'
                },
                listeners: {
                    change: function (combo) {
                        var text = combo.getValue();
                    },
                    select: function (combo, record) {
                        var viewmodel = me.parent.getViewModel(),
                            store = combo.getStore();
                        var data = record.getData();
                        viewmodel.set('employee', data);
                    },
                    beforequery: function (record) {
                        record.query = new RegExp(record.query, 'i');
                        record.forceAll = true;
                    }
                }, triggers: {
                    clear: {
                        weight: 1,
                        cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                        hidden: false,
                        handler: function (el) {                
                            el.clearValue();
                            var viewmodel = me.parent.getViewModel();
                            viewmodel.set('employee', null);
                        },
                        scope: 'this'
                    }
                }
            }, {
                fieldLabel: 'First Name',
                columnWidth: .3,
                allowBlank: false,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.firstName}'
                }
            }, {
                fieldLabel: 'Last Name',
                columnWidth: .3,
                allowBlank: false,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.lastName}'
                }
            }, {
                fieldLabel: 'Display Name',
                columnWidth: 1,
                allowBlank: false,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.employeeName}'
                }
            }, {
                fieldLabel: 'Job Tile',
                columnWidth: .4,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.position}'
                }
            }, {
                fieldLabel: 'Department',
                columnWidth: .3,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.department}'
                }
            }, {
                fieldLabel: 'Phone(Ext)',
                columnWidth: .3,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.phone}'
                }
            }, {
                fieldLabel: 'Office Location',
                columnWidth: .7,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.location}'
                }
            }, {
                fieldLabel: 'Mobile(H/P)',
                columnWidth: .3,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.mobile}'
                }
            }, {
                fieldLabel: 'Email Address',
                columnWidth: .7,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.email}',
                    disabled: '{emailDisable}'
                }
            }, {
                xtype: 'checkbox',
                boxLabel: 'No Email',
                reference: 'noEmail',
                columnWidth: .3,
                hideLabel: true,
                checked: false,
                handler: function (el) {
                    var viewmodel = me.parent.getViewModel();
                    if (el.getValue()) {
                        viewmodel.set('emailDisable', true);
                        viewmodel.set('employee.email', null);
                    } else {
                        viewmodel.set('emailDisable', false);
                    }                        
                },
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.noEmail}'
                }
            }, {
                xtype: 'textarea',
                fieldLabel: 'Remark',
                margin: '10 10',
                columnWidth: 1,
                bind: {
                    readOnly: '{config.readOnly}',
                    value: '{employee.remark}'
                }
            }
        ];

        me.callParent(arguments);
    }
});
