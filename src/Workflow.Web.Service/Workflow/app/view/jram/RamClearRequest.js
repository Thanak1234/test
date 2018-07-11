/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.jram.RamClearRequest", {
    extend: "Workflow.view.FormComponent",
    xtype: 'jram-ramclear-request',
    modelName: 'ramClear',
    layout: 'vbox',
    defaults: {
        defaultType: 'textfield'
    },
    loadData: function (data, viewSetting) {
        var me = this,
            viewmodel = me.getViewModel(),
            reference = me.getReferences();

        if (data) {
            var ramClearProperty = viewmodel.get('ramClearProperty');
            viewmodel.set('ramClear', data.ramClear);
        }
    },
    buildComponent: function () {
        var me = this;
        return [{
            xtype: 'combobox',
            fieldLabel: 'Property',
            displayField: 'label',
            editable: false,
            allowBlank: false,
            store: Ext.create('Ext.data.Store', {
                data: [
                    { label: 'Naga 1' },
                    { label: 'Naga 2' }
                ]
            }),
            bind: {
                value: '{ramClear.props}'
            }
        }, {
            xtype: 'fieldcontainer',
            layout: 'hbox',
            items: [{
                width: 400,
                xtype: 'fieldcontainer',
                layout: 'vbox',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: 'GMID',
                    allowBlank: false,
                    bind: {
                        value: '{ramClear.gmid}'
                    }
                },
                {
                    xtype: 'textfield',
                    fieldLabel: 'Game',
                    allowBlank: false,
                    bind: {
                        value: '{ramClear.game}'
                    }
                }]
            }, {
                width: 400,
                xtype: 'fieldcontainer',
                layout: 'vbox',
                items: [{
                    fieldLabel: 'RTP% current',
                    allowBlank: false,
                    xtype: 'numberfield',
                    minValue: 0.00000001,
                    maxValue: 100,
                    allowDecimals: true,
                    bind: {
                        value: '{ramClear.rtp}'
                    }
                },
                {
                    fieldLabel: 'Date',
                    xtype: 'datefield',
                    format: 'd-M-y',
                    bind: {
                        value: '{ramClear.clearDate}'
                    }
                }]
            }]
        }];
    }
});