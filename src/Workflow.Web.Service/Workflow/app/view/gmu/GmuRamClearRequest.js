Ext.define("Workflow.view.gmu.GmuRamClearRequest", {
    extend: "Workflow.view.FormComponent",
    xtype: 'gmu-ramclear-request',
    modelName: 'gmuRamClear',
    layout: 'anchor',
    defaults: {
        anchor: '100%'
    },
    fieldDefaults: {
        labelAlign: 'right',
        labelWidth: 115,
        msgTarget: 'side'
    },
    loadData: function (data, viewSetting) {
        var me = this,
            viewmodel = me.getViewModel(),
            reference = me.getReferences();

        if (data) {
            var gmuRamClearProperty = viewmodel.get('gmuRamClearProperty');
            viewmodel.set('gmuRamClear', data.gmuRamClear);
        }
    },
    afterRender: function () {
        var me = this;
        vm = me.getViewModel();
        vm.set('gmuRamClear.disabledMacAddress', vm.get('gmuRamClear.disabledMacAddress') ? vm.get('gmuRamClear.disabledMacAddress') : true);
        vm.set('gmuRamClear.disabledDescr', vm.get('gmuRamClear.disabledDescr') ? vm.get('gmuRamClear.disabledDescr') : true);
        vm.set('gmuRamClear.hiddenGmu', vm.get('gmuRamClear.hiddenGmu') ? vm.get('gmuRamClear.hiddenGmu') : true);
        me.callParent(arguments);
    },
    buildComponent: function () {
        var me = this;

        return [{
            xtype: 'fieldset',
            title: 'Information',
            defaultType: 'textfield',
            defaults: {
                anchor: '100%'
            },
            items: [{
                xtype: 'combobox',
                fieldLabel: 'Property',
                displayField: 'label',
                maxWidth: 350,
                editable: false,
                allowBlank: false,
                store: Ext.create('Ext.data.Store', {
                    data: [
                        { label: 'Naga 1' },
                        { label: 'Naga 2' }
                    ]
                }),
                bind: {
                    value: '{gmuRamClear.props}'
                }
            }, {
                xtype: 'textfield',
                fieldLabel: 'GMID',
                maxWidth: 350,
                allowBlank: false,
                bind: {
                    value: '{gmuRamClear.gmid}'
                }
            }, {
                xtype: 'checkboxgroup',
                cls: 'x-check-group-alt',
                columns: [450],
                margin: '0 0 0 120',
                vertical: true,
                bind: {
                    value: '{gmuRamClear.gmus}'
                },
                listeners: {
                    change: function (field, v, oldValue, eOpts) {
                        var vm = me.getViewModel();
                        var enabledMacAddress = (v.gmus == 2 || (v.gmus && v.gmus.length > 0 && v.gmus.indexOf(2) > -1));
                        var disabledDescr = !(v.gmus == 3 || (v.gmus && v.gmus.length > 0 && v.gmus.indexOf(3) > -1));

                        vm.set('gmuRamClear.disabledDescr', disabledDescr);
                        vm.set('gmuRamClear.disabledMacAddress', !enabledMacAddress);
                    }
                },
                items: [{
                    boxLabel: 'RAM CLEAR',
                    name: 'gmus',
                    inputValue: 1
                }, {
                    boxLabel: 'GMU CHANGE',
                    name: 'gmus',
                    inputValue: 2
                    //checked: true
                }, {
                    xtype: 'textfield',
                    allowBlank: false,
                    minWidth: 230,
                    fieldStyle: 'text-transform:uppercase',
                    bind: {
                        disabled: '{gmuRamClear.disabledMacAddress}',
                        value: '{gmuRamClear.macAddress}',
                        emptyText: 'GMU MAC'
                    }
                }, {
                    boxLabel: 'Others (description below)',
                    name: 'gmus',
                    inputValue: 3
                }]
            }, {
                xtype: 'textarea',
                margin: '20 0 10',
                fieldLabel: 'Description',
                bind: {
                    disabled: '{gmuRamClear.disabledDescr}',
                    value: '{gmuRamClear.descr}'
                }
            }]
        }, {
            xtype: 'fieldset',
            title: 'IT Configuration',
            defaultType: 'textfield',
            defaults: {
                anchor: '100%'
            },
            bind: {
                hidden: '{gmuRamClearProperty.ip.hidden}'
            },
            items: [{
                fieldLabel: 'IP',
                allowBlank: false,
                xtype: 'textfield',
                bind: {
                    disabled: '{gmuRamClear.disabledMacAddress}',
                    value: '{gmuRamClear.ip}'
                }
            }, {
                xtype: 'textarea',
                fieldLabel: 'Remark',
                minWidth: 580,
                bind: {
                    disabled: '{gmuRamClear.disabledMacAddress}',
                    value: '{gmuRamClear.remark}'
                }
            }]
        }];
    }
});